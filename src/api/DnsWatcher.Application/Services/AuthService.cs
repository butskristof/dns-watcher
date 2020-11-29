using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Auth;
using DnsWatcher.Application.Contracts.Dto.Auth;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Configuration;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DnsWatcher.Application.Services
{
	internal class AuthService : IAuthService
	{
		private readonly IDnsWatcherDbContext _context;
		private readonly IJwtHelper _jwtHelper;
		private readonly IRandomGenerator _randomGenerator;
		private readonly IDateTime _dateTime;
		private readonly IJwtOptions _jwtOptions;

		public AuthService(IJwtHelper jwtHelper,
			IDnsWatcherDbContext context,
			IRandomGenerator randomGenerator,
			IDateTime dateTime,
			IJwtOptions jwtOptions)
		{
			_jwtHelper = jwtHelper;
			_context = context;
			_randomGenerator = randomGenerator;
			_dateTime = dateTime;
			_jwtOptions = jwtOptions;
		}

		public async Task<TokenDto> Authenticate(LoginData data)
		{
			var user = await _context
							.Users
							.Include(e => e.RefreshTokens)
							.FirstOrDefaultAsync(e => e.Username == data.Username && e.Active)
						?? throw new NotFoundException($"No user for username {data.Username}");

			if (!VerifyPasswordHash(data.Password, user.PasswordHash, user.PasswordSalt))
				throw new ArgumentException("Invalid password.");

			var token = _jwtHelper.GenerateJwtToken(user);

			var referenceTime = _dateTime.Now;
			var refreshToken = user.RefreshTokens
				.FirstOrDefault(e => e.Revoked == null
									&& referenceTime < e.Expires);
			if (refreshToken == null)
			{
				refreshToken = CreateRefreshToken();
				user.RefreshTokens.Add(refreshToken);

				await _context.SaveChangesAsync();
			}

			token.RefreshToken = refreshToken.Token;
			token.RefreshTokenValidUntil = refreshToken.Expires;

			return token;
		}

		public async Task Register(RegisterData data)
		{
			if (await _context
				.Users
				.AnyAsync(e => e.Username == data.Username))
				throw new ArgumentException("Username already taken.");

			CreatePasswordHash(data.Password, out var hash, out var salt);
			var user = new User
			{
				Username = data.Username,
				PasswordHash = hash,
				PasswordSalt = salt
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
		}

		public async Task<TokenDto> RefreshToken(RefreshData data)
		{
			var user = await _context
							.Users
							.Include(e => e.RefreshTokens)
							.SingleOrDefaultAsync(e => e.RefreshTokens
								.Any(t => t.Token == data.RefreshToken))
						?? throw new ForbiddenException($"Invalid refresh token.");
			var token = user.RefreshTokens
				.Single(e => e.Token == data.RefreshToken);
			if (token.Revoked != null || _dateTime.Now >= token.Expires)
			{
				throw new ForbiddenException("Invalid refresh token");
			}

			// revoke current
			token.Revoked = _dateTime.Now;
			// generate new
			var newToken = CreateRefreshToken();
			user.RefreshTokens.Add(newToken);
			await _context.SaveChangesAsync();

			// return new access + refresh
			var r = _jwtHelper.GenerateJwtToken(user);
			r.RefreshToken = newToken.Token;
			r.RefreshTokenValidUntil = newToken.Expires;

			return r;
		}

		public async Task RevokeTokens(RevokeTokenData data)
		{
			var user = await _context
							.Users
							.Include(e => e.RefreshTokens)
							.FirstOrDefaultAsync(e => e.Username == data.Username && e.Active)
						?? throw new NotFoundException($"No user for username {data.Username}");

			var referenceTime = _dateTime.Now;
			user.RefreshTokens
				.Where(e => e.Revoked == null)
				.ToList()
				.ForEach(t =>
				{
					t.Revoked = referenceTime;
				});

			await _context.SaveChangesAsync();
		}

		private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			if (storedHash.Length != 64)
				throw new ArgumentException("Invalid password hash length.");
			if (storedSalt.Length != 128)
				throw new ArgumentException("Invalid salt length");

			using var hmac = new HMACSHA512(storedSalt);
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			for (var i = 0; i < computedHash.Length; ++i)
				if (computedHash[i] != storedHash[i])
					return false;

			return true;
		}

		private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
		{
			using var hmac = new HMACSHA512();
			salt = hmac.Key;
			hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
		}

		private RefreshToken CreateRefreshToken()
		{
			var token = _randomGenerator.GetRandomBase64ByteString(32);
			return new RefreshToken
			{
				Token = token,
				Expires = _dateTime.Now.AddDays(_jwtOptions.RefreshTokenExpireDays)
			};
		}
	}
}