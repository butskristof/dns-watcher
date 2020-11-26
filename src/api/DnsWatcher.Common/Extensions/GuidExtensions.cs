using System;

namespace DnsWatcher.Common.Extensions
{
	public static class GuidExtensions
	{
		public static string ToSafeString(this Guid guid)
		{
			return guid.ToString().ToLowerInvariant();
		}
	}
}