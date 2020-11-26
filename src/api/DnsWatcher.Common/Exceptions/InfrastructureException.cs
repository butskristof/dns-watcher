using System;

namespace DnsWatcher.Common.Exceptions
{
	public class InfrastructureException : Exception
	{
		public InfrastructureException(string message) : base(message)
		{
		}
	}
}