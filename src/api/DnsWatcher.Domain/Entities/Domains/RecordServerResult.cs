using System;
using DnsWatcher.Domain.Common;
using DnsWatcher.Domain.Entities.Servers;

namespace DnsWatcher.Domain.Entities.Domains
{
	public class RecordServerResult : EntityBase
	{
		public Guid WatchedRecordId { get; set; }
		public WatchedRecord WatchedRecord { get; set; }

		public Guid DnsServerId { get; set; }
		public DnsServer DnsServer { get; set; }

		public string IpAddress { get; set; }
		public int TimeToLive { get; set; }
	}
}