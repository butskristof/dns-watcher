using System;
using DnsWatcher.Common.Enumerations;

namespace DnsWatcher.Common.Exceptions
{
	public class BadDataException : Exception
	{
		public BadDataException(ErrorCode code) : base(code.ToString())
		{
		}
	}
}