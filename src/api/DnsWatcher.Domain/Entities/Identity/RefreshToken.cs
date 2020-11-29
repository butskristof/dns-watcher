using System;
using DnsWatcher.Domain.Common;

namespace DnsWatcher.Domain.Entities.Identity
{
	public class RefreshToken : EntityBase
	{
		public string Token { get; set; }
		public DateTime Expires { get; set; }
		public DateTime? Revoked { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}