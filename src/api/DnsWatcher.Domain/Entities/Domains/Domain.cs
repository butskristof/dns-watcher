using DnsWatcher.Domain.Common;

namespace DnsWatcher.Domain.Entities.Domains
{
	public class Domain : EntityBase
	{
		public string DomainName { get; set; }
	}
}