namespace DnsWatcher.Common.Configuration
{
	public interface IEncryptionOptions
	{
		string Key { get; }
	}

	public class EncryptionOptions : IEncryptionOptions
	{
		public string Key { get; set; } // should be 32 chars
	}
}