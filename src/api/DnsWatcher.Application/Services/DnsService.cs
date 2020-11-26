using System.Linq;
using System.Threading.Tasks;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Helpers;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Domain.Entities.Domains;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Application.Services
{
	public class DnsService : IDnsService
	{
		#region construction

		private readonly IDnsWatcherDbContext _context;
		private readonly ILogger<DnsService> _logger;
		private readonly IDnsServersService _dnsServersService;
		private readonly IDnsResolver _dnsResolver;

		public DnsService(IDnsWatcherDbContext context, 
			ILogger<DnsService> logger, 
			IDnsServersService dnsServersService, 
			IDnsResolver dnsResolver)
		{
			_context = context;
			_logger = logger;
			_dnsServersService = dnsServersService;
			_dnsResolver = dnsResolver;
		}

		#endregion
		
		public async Task UpdateDnsResultsForRecordAsync(WatchedRecord record)
		{
			var query = !string.IsNullOrWhiteSpace(record.Prefix)
				? $"{record.Prefix}.{record.WatchedDomain.DomainName}"
				: record.WatchedDomain.DomainName;

			var servers = await _dnsServersService.GetDnsServersAsync();
			
			foreach (var server in servers.DnsServers)
			{
				var data = new DnsQueryData
				{
					Domain = query,
					RecordType = record.RecordType,
					Ip = server.IpAddress,
					Port = server.Port
				};
				var result = await _dnsResolver.ResolveDnsQuery(data);

				var existingResult = record.Results
					.FirstOrDefault(e => e.DnsServerId == server.Id);
				if (existingResult != null)
				{
					existingResult.IpAddress = result.Value;
					existingResult.TimeToLive = result.TimeToLive;
				}
				else
				{
					var newResult = new RecordServerResult
					{
						IpAddress = result.Value,
						TimeToLive = result.TimeToLive,
						DnsServerId = server.Id
					};
					record.Results.Add(newResult);
				}
			}

			await _context.SaveChangesAsync();
		}
	}
}