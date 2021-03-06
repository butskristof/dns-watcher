﻿using System;
using System.Collections.Generic;
using DnsWatcher.Domain.Common;
using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Domain.Entities.Domains
{
	public class WatchedRecord : EntityBase
	{
		public RecordType RecordType { get; set; }
		public string Prefix { get; set; }
		public string ExpectedValue { get; set; }
		public int ExpectedTimeToLive { get; set; }
		
		public Guid WatchedDomainId { get; set; }
		public WatchedDomain WatchedDomain { get; set; }

		public ICollection<RecordServerResult> Results { get; set; }
	}
}