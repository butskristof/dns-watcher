using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DnsWatcher.Common.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DnsWatcher.API.Helpers
{
	internal class ApiAuthenticationInfo : IAuthenticationInfo
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ApiAuthenticationInfo(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public Guid? UserId
		{
			get
			{
				var userId = _httpContextAccessor
					.HttpContext?
					.User?
					.Claims?
					.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Sub)?
					.Value;
				if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var userGuid))
					return null;
				return userGuid;
			}
		}
	}
}