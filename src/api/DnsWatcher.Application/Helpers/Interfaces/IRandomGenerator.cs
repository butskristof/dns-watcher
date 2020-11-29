namespace DnsWatcher.Application.Helpers.Interfaces
{
	public interface IRandomGenerator
	{
		string GetRandomBase64ByteString(int numberOfBytes);
	}
}