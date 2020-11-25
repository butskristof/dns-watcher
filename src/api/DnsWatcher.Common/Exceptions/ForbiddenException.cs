using System;

namespace DnsWatcher.Common.Exceptions
{
	public class ForbiddenException : Exception
	{
		public ForbiddenException(string message) : base(message)
		{
		}
	}
}