using System;

namespace DnsWatcher.Application.Contracts.Data.Servers
{
	public class UpdateDnsServerData : DnsServerData
	{
		public Guid Id { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}