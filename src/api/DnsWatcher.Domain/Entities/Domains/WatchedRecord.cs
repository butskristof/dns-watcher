using System;
using DnsWatcher.Domain.Common;
using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Domain.Entities.Domains
{
	public class WatchedRecord : EntityBase
	{
		public RecordType RecordType { get; set; }
		public string ExpectedIpAddress { get; set; }
		public int ExpectedPort { get; set; }
		
		public Guid WatchedDomainId { get; set; }
		public WatchedDomain WatchedDomain { get; set; }
	}
}