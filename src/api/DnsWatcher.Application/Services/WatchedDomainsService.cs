using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Domain.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Application.Services
{
	internal class WatchedDomainsService : IWatchedDomainsService
	{
		#region construction

		private readonly ILogger<WatchedDomainsService> _logger;
		private readonly IDnsWatcherDbContext _context;
		private readonly IMapper _mapper;

		public WatchedDomainsService(ILogger<WatchedDomainsService> logger, 
			IDnsWatcherDbContext context, 
			IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}

		#endregion
		
		public async Task<WatchedDomainsDto> GetWatchedDomainsAsync()
		{
			var domains = await _context.WatchedDomains
				.OrderBy(e => e.DomainName)
				.ToListAsync();
			
			return new WatchedDomainsDto
			{
				Domains = _mapper.Map<IEnumerable<WatchedDomainDto>>(domains)
			};
		}

		public async Task<WatchedDomain> GetDomainAsync(Guid id, bool includeResults = false)
		{
			var query = _context.WatchedDomains
				.Include(e => e.WatchedRecords).ThenInclude(e => e.Results)
				.AsQueryable();
			if (includeResults)
			{
				query = query
					.Include(e => e.WatchedRecords).ThenInclude(e => e.Results);
			}
			return await query
				.FirstOrDefaultAsync(e => e.Id == id);
		}
		
		public async Task<WatchedDomainDto> GetWatchedDomainByIdAsync(Guid id)
		{
			var domain = await GetDomainAsync(id)
						?? throw new NotFoundException($"No WatchedDomain found with id {id}.");
			return _mapper.Map<WatchedDomainDto>(domain);
		}

		public async Task<WatchedDomainDto> CreateWatchedDomainAsync(CreateWatchedDomainData data)
		{
			var domain = _mapper.Map<WatchedDomain>(data);

			_context.WatchedDomains.Add(domain);
			await _context.SaveChangesAsync();

			return _mapper.Map<WatchedDomainDto>(domain);
		}

		public async Task<WatchedDomainDto> UpdateWatchedDomainAsync(UpdateWatchedDomainData data)
		{
			var domain = await GetDomainAsync(data.Id)
				?? throw new NotFoundException($"No WatchedDomain found with id {data.Id}.");

			_mapper.Map(data, domain);
			
			await _context.SaveChangesAsync();

			return _mapper.Map<WatchedDomainDto>(domain);
		}

		public async Task DeleteWatchedDomainAsync(Guid id)
		{
			var domain = await GetDomainAsync(id, true)
				?? throw new NotFoundException($"No WatchedDomain found with id {id}.");

			_context.WatchedDomains.Remove(domain);
			await _context.SaveChangesAsync();
		}
	}
}