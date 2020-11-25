using System;
using DnsWatcher.Application.Common.Interfaces;

namespace DnsWatcher.Infrastructure.Services
{
	internal class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.UtcNow;
	}
}