using System.Collections.Generic;

namespace DnsWatcher.Application.Contracts.Dto.Servers
{
	public class DnsServersDto
	{
		public IEnumerable<DnsServerDto> DnsServers { get; set; }
	}
}