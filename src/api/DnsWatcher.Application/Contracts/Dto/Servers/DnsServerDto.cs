using DnsWatcher.Application.Contracts.Mappings;
using DnsWatcher.Domain.Entities.Servers;

namespace DnsWatcher.Application.Contracts.Dto.Servers
{
	public class DnsServerDto : EntityBaseDto, IMapFrom<DnsServer>
	{
		public string Name { get; set; }
		public string IpAddress { get; set; }
		public int Port { get; set; }
	}
}