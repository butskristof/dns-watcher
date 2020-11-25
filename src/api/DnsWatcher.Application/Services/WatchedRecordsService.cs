using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Domain.Entities.Domains;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Application.Services
{
	internal class WatchedRecordsService : IWatchedRecordsService
	{

		#region construction

		private readonly ILogger<WatchedRecordsService> _logger;
		private readonly IDnsWatcherDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWatchedDomainsService _domainsService;

		public WatchedRecordsService(ILogger<WatchedRecordsService> logger, 
			IDnsWatcherDbContext context, 
			IMapper mapper, 
			IWatchedDomainsService domainsService)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
			_domainsService = domainsService;
		}

		#endregion

		public async Task<WatchedRecordDto> CreateWatchedRecordAsync(Guid domainId, CreateWatchedRecordData data)
		{
			var domain = await _domainsService.GetDomainAsync(domainId);
			var record = _mapper.Map<WatchedRecord>(data);
			domain.WatchedRecords.Add(record);
			await _context.SaveChangesAsync();
			return _mapper.Map<WatchedRecordDto>(record);
		}

		public async Task<WatchedRecordDto> UpdateWatchedRecordAsync(Guid domainId, UpdateWatchedRecordData data)
		{
			var domain = await _domainsService.GetDomainAsync(domainId);
			var record = domain.WatchedRecords
				.FirstOrDefault(e => e.Id == data.Id)
				?? throw new NotFoundException($"WatchedRecord with {data.Id} not found in WatchedDomain {domainId}");

			_mapper.Map(data, record);
			
			await _context.SaveChangesAsync();
			
			return _mapper.Map<WatchedRecordDto>(record);
		}

		public async Task DeleteWatchedRecordAsync(Guid domainId, Guid id)
		{
			var domain = await _domainsService.GetDomainAsync(domainId);
			var record = domain.WatchedRecords
				.FirstOrDefault(e => e.Id == id)
				?? throw new NotFoundException($"WatchedRecord with {id} not found in WatchedDomain {domainId}");

			_context.WatchedRecords.Remove(record);
			await _context.SaveChangesAsync();
		}
	}
}