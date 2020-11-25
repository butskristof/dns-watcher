using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Dto.Auth;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Common.Configuration;
using DnsWatcher.Common.Extensions;
using DnsWatcher.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DnsWatcher.Application.Helpers
{
	internal class JwtHelper : IJwtHelper
	{
		private readonly IDateTime _dateTime;
		private readonly IJwtOptions _jwtOptions;

		public JwtHelper(IJwtOptions jwtOptions, IDateTime dateTime)
		{
			_jwtOptions = jwtOptions;
			_dateTime = dateTime;
		}

		public TokenDto GenerateJwtToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToSafeString())
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = _dateTime.Now.AddSeconds(Convert.ToDouble(_jwtOptions.ExpireSeconds));

			var token = new JwtSecurityToken(
				_jwtOptions.Issuer,
				_jwtOptions.Issuer,
				claims,
				expires: expires,
				signingCredentials: signingCredentials
			);
			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return new TokenDto
			{
				Token = tokenString,
				ValidUntil = expires,
				UserId = user.Id.ToSafeString(),
				Username = user.Username
			};
		}
	}
}