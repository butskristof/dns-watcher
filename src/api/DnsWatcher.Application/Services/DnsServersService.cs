using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Contracts.Dto.Servers;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Application.Services
{
	public class DnsServersService : IDnsServersService
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

		public async Task<DnsServerDto> GetDnsServerByIdAsync(Guid id)
		{
			var server = await _context.DnsServers
				.FindAsync(id)
				?? throw new NotFoundException($"No DnsServer found with id {id}.");
			return _mapper.Map<DnsServerDto>(server);
		}

		public Task<DnsServerDto> CreateDnsServer(CreateDnsServerData data)
		{
			throw new NotImplementedException();
		}
	}
}