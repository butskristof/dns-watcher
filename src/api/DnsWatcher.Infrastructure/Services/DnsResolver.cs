using System.Collections.Generic;
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
		private readonly Dictionary<(IPAddress, int), ILookupClient> _clients;

		public DnsResolver(ILogger<DnsResolver> logger)
		{
			_logger = logger;
			_clients = new Dictionary<(IPAddress, int), ILookupClient>();
		}

		#endregion

		#region setup

		private ILookupClient GetClient(string ip, int port)
		{
			var parsedIp = IPAddress.Parse(ip);
			var key = (parsedIp, port);

			var clientExists = _clients.TryGetValue(key, out var client);
			if (!clientExists)
			{
				client = new LookupClient(key.parsedIp, key.port);
				_clients.Add(key, client);
			}

			return client;
		}

		#endregion

		public async Task<DnsQueryResult> ResolveDnsQuery(DnsQueryData data)
		{
			_logger.LogInformation(
				$"Resolving DNS request for {data.RecordType} {data.Domain} with {data.Ip}:{data.Port}");
			var client = GetClient(data.Ip, data.Port);

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
			var res = new DnsQueryResult {TimeToLive = -1};

			if (result != null)
			{
				res.TimeToLive = result.TimeToLive;
				if (result is ARecord a)
				{
					res.Value = a.Address.ToString();
				}
				else if (result is AaaaRecord aaaa)
				{
					res.Value = aaaa.Address.ToString();
				}
				else if (result is CNameRecord cname)
				{
					res.Value = cname.CanonicalName.ToString();
				}
				else if (result is MxRecord mx)
				{
					res.Value = mx.Exchange.ToString();
				}
				else if (result is TxtRecord txt)
				{
					res.Value = txt.Text.ToString();
				}
			}

			return res;
		}
	}
}