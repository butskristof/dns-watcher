using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DnsClient;
using DnsClient.Protocol;
using DnsWatcher.Application.Contracts.Data.Helpers;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Domain.Enumerations;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Infrastructure.Services
{
	public class DnsResolver : IDnsResolver
	{
		#region construction
		
		private readonly ILogger<DnsResolver> _logger;

		public DnsResolver(ILogger<DnsResolver> logger)
		{
			_logger = logger;
		}

		#endregion

		public async Task<DnsQueryResult> ResolveDnsQuery(DnsQueryData data)
		{
			_logger.LogInformation($"Resolving DNS request for {data.RecordType} {data.Domain} with {data.Ip}:{data.Port}");
			var ip = IPAddress.Parse(data.Ip);
			var client = new LookupClient(ip, data.Port);

			var type = MapQueryType(data.RecordType);

			var result = (await client.QueryAsync(data.Domain, type)).Answers.FirstOrDefault();
			return ProcessResult(result);
		}

		private QueryType MapQueryType(RecordType type)
		{
			return type switch
			{
				RecordType.A => QueryType.A,
				RecordType.AAAA => QueryType.AAAA,
				RecordType.CNAME => QueryType.CNAME,
				RecordType.MX => QueryType.MX,
				RecordType.TXT => QueryType.TXT,
				_ => QueryType.A
			};
		}

		private DnsQueryResult ProcessResult(DnsResourceRecord result)
		{
			var res = new DnsQueryResult { TimeToLive = -1 };
			if (result is ARecord a)
			{
				res.Value = a.Address.ToString();
				res.TimeToLive = a.TimeToLive;
			}
			return res;
		}
	}
}