namespace DnsWatcher.Common.Configuration
{
	public interface IJwtOptions
	{
		string Key { get; }
		string Issuer { get; }
		int ExpireSeconds { get; }
	}

	public class JwtOptions : IJwtOptions
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public int ExpireSeconds { get; set; }
	}
}