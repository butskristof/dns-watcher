using DnsWatcher.Application.Contracts.Mappings;
using DnsWatcher.Domain.Entities.Domains;

namespace DnsWatcher.Application.Contracts.Dto.Domains
{
	public class WatchedDomainDto : EntityBaseDto, IMapFrom<WatchedDomain>
	{
		public string DomainName { get; set; }
	}
}