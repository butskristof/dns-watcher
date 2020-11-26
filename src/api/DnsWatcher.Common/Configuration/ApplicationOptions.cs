namespace DnsWatcher.Common.Configuration
{
	public interface IApplicationOptions
	{
		string FrontEndUrls { get; }
		string Build { get; }
		string Version { get; }
		string Environment { get; }
	}

	public class ApplicationOptions : IApplicationOptions
	{
		public string FrontEndUrls { get; set; }
		public string Build { get; set; }
		public string Version { get; set; }
		public string Environment { get; set; }
	}
}