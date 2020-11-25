using System;

namespace DnsWatcher.Application.Contracts.Data.Domains
{
	public abstract class WatchedDomainData
	{
		public string DomainName { get; set; }
	}
	
	public class CreateWatchedDomainData : WatchedDomainData
	{
	}
	
	public class UpdateWatchedDomainData : WatchedDomainData
	{
		public Guid Id { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}