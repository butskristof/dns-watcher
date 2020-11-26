namespace DnsWatcher.Common.Helpers.Interfaces
{
	public interface IEncryptionHelper
	{
		string Encrypt(string value);
		string Decrypt(string cipherText);
	}
}