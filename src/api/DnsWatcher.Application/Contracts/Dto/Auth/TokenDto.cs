using System;

namespace DnsWatcher.Application.Contracts.Dto.Auth
{
	public class TokenDto
	{
		public string UserId { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }
		public DateTime ValidUntil { get; set; }
	}
}