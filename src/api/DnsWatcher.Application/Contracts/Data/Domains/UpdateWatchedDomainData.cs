using System;

namespace DnsWatcher.Application.Contracts.Data.Domains
{
	public class UpdateWatchedDomainData : WatchedDomainData
	{
		public Guid Id { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}