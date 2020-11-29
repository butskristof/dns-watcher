using System.Collections.Generic;
using DnsWatcher.Domain.Common;

namespace DnsWatcher.Domain.Entities.Identity
{
	public class User : EntityBase
	{
		public string Username { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public bool Active { get; } = false;

		public ICollection<RefreshToken> RefreshTokens { get; set; }
	}
}