using System.Collections.Generic;

namespace DnsWatcher.Application.Contracts.Dto.Domains
{
	public class WatchedDomainsDto
	{
		public IEnumerable<WatchedDomainDto> Domains { get; set; }
	}
}