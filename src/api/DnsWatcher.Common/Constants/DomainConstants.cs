using System.Text.RegularExpressions;

namespace DnsWatcher.Common.Constants
{
	public static class DomainConstants
	{
		public const int Zero = 0;
		public const int MinimumPort = 1;
		public const int MaximumPort = 65536;

		// https://stackoverflow.com/a/26987741
		public static readonly Regex DomainRegex = new Regex(
			@"^(((?!-))(xn--|_{1,1})?[a-z0-9-]{0,61}[a-z0-9]{1,1}\.)*(xn--)?([a-z0-9][a-z0-9\-]{0,60}|[a-z0-9-]{1,30}\.[a-z]{2,})$",
			RegexOptions.Compiled);

		// https://stackoverflow.com/a/36760050
		public static readonly Regex IpRegex = new Regex(
			@"^((25[0-5]|(2[0-4]|1[0-9]|[1-9]|)[0-9])(\.(?!$)|$)){4}$",
			RegexOptions.Compiled);
	}
}