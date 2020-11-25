using DnsWatcher.Domain.Common;

namespace DnsWatcher.Domain.Entities.Servers
{
	public class DnsServer : EntityBase
	{
		public string Name { get; set; }
		public string IpAddress { get; set; }
		public int Port { get; set; } = 53;
	}
}