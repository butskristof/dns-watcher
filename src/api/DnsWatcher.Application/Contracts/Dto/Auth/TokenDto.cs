using System;

namespace DnsWatcher.Application.Contracts.Dto.Auth
{
	public class TokenDto
	{
		public string UserId { get; set; }
		public string Username { get; set; }
		public string AccessToken { get; set; }
		public DateTime AccessTokenValidUntil { get; set; }
		public string RefreshToken { get; set; }
		public DateTime RefreshTokenValidUntil { get; set; }
	}
}