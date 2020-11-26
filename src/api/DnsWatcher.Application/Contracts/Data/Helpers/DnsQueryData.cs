using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Application.Contracts.Data.Helpers
{
	public class DnsQueryData
	{
		public string Ip { get; set; }
		public int Port { get; set; }
		public string Domain { get; set; }
		public RecordType RecordType { get; set; }
	}
}