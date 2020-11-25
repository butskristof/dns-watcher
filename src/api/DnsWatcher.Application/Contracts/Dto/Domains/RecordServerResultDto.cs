using DnsWatcher.Application.Contracts.Dto.Servers;
using DnsWatcher.Application.Contracts.Mappings;
using DnsWatcher.Domain.Entities.Domains;

namespace DnsWatcher.Application.Contracts.Dto.Domains
{
	public class RecordServerResultDto : EntityBaseDto, IMapFrom<RecordServerResult>
	{
		public string IpAddress { get; set; }
		public int TimeToLive { get; set; }
		public DnsServerDto DnsServer { get; set; }
	}
}