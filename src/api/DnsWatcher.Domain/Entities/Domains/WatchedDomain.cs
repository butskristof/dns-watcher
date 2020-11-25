using DnsWatcher.Domain.Common;

namespace DnsWatcher.Domain.Entities.Domains
{
	public class WatchedDomain : EntityBase
	{
		public string DomainName { get; set; }
	}
}