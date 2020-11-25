using System;
using DnsWatcher.Common.Enumerations;

namespace DnsWatcher.Common.Exceptions
{
	public class DataChangedException : Exception
	{
		public DataChangedException() : base(ErrorCode.DataChanged.ToString())
		{
		}
	}
}