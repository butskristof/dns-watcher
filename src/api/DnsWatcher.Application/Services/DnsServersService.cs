using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Contracts.Dto.Servers;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Domain.Entities.Servers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Application.Services
{
	internal class DnsServersService : IDnsServersService
	{
		#region construction
		
		private readonly ILogger<DnsServersService> _logger;
		private readonly IDnsWatcherDbContext _context;
		private readonly IMapper _mapper;

		public DnsServersService(ILogger<DnsServersService> logger, 
			IDnsWatcherDbContext context, 
			IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}

		#endregion

		public async Task<DnsServersDto> GetDnsServersAsync()
		{
			var servers = await _context.DnsServers
				.ToListAsync();
			
			return new DnsServersDto
			{
				DnsServers = _mapper.Map<IEnumerable<DnsServerDto>>(servers)
			};
		}

		private async Task<DnsServer> GetDnsServerAsync(Guid id)
		{
			return await _context.DnsServers
				.FindAsync(id);
		}

		public async Task<DnsServerDto> GetDnsServerByIdAsync(Guid id)
		{
			var server = await GetDnsServerAsync(id)
				?? throw new NotFoundException($"No DnsServer found with id {id}.");
			return _mapper.Map<DnsServerDto>(server);
		}

		public async Task<DnsServerDto> CreateDnsServerAsync(CreateDnsServerData data)
		{
			var server = _mapper.Map<DnsServer>(data);

			_context.DnsServers.Add(server);
			await _context.SaveChangesAsync();

			return _mapper.Map<DnsServerDto>(server);
		}

		public async Task<DnsServerDto> UpdateDnsServerAsync(UpdateDnsServerData data)
		{
			var server = await GetDnsServerAsync(data.Id)
				?? throw new NotFoundException($"No DnsServer found with id {data.Id}.");

			_mapper.Map(data, server);
			
			await _context.SaveChangesAsync();

			return _mapper.Map<DnsServerDto>(server);
		}

		public async Task DeleteDnsServerAsync(Guid id)
		{
			var server = await GetDnsServerAsync(id)
				?? throw new NotFoundException($"No DnsServer found with id {id}.");

			_context.DnsServers.Remove(server);
			await _context.SaveChangesAsync();
		}
	}
}