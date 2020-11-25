using System;

namespace DnsWatcher.Common.Helpers.Interfaces
{
	public interface IAuthenticationInfo
	{
		Guid? UserId { get; }
	}
}