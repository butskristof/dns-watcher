namespace DnsWatcher.Application.Contracts.Data.Servers
{
	public abstract class DnsServerData
	{
		public string Name { get; set; }
		public string IpAddress { get; set; }
		public int Port { get; set; }
	}
}