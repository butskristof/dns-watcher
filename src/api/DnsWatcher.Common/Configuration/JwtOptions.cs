namespace DnsWatcher.Common.Configuration
{
	public interface IJwtOptions
	{
		string Key { get; }
		string Issuer { get; }
		int AccessTokenExpireSeconds { get; }
		int RefreshTokenExpireDays { get; }
	}

	public class JwtOptions : IJwtOptions
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public int AccessTokenExpireSeconds { get; set; }
		public int RefreshTokenExpireDays { get; set; }
	}
}