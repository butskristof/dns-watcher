using System;
using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Application.Contracts.Data.Domains
{
	public abstract class WatchedRecordData
	{
		public RecordType RecordType { get; set; }
		public string Prefix { get; set; }
		public string ExpectedValue { get; set; }
		public int ExpectedTimeToLive { get; set; }
	}

	public class CreateWatchedRecordData : WatchedRecordData
	{
	}

	public class UpdateWatchedRecordData : WatchedRecordData
	{
		public Guid Id { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}