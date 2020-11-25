using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Application.Contracts.Data.Auth;
using DnsWatcher.Application.Contracts.Dto.Auth;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DnsWatcher.Application.Services
{
	internal class AuthService : IAuthService
	{
		private readonly IDnsWatcherDbContext _context;
		private readonly IJwtHelper _jwtHelper;

		public AuthService(IJwtHelper jwtHelper,
			IDnsWatcherDbContext context)
		{
			_jwtHelper = jwtHelper;
			_context = context;
		}

		public async Task<TokenDto> Authenticate(LoginData data)
		{
			var user = await _context
							.Users
							.FirstOrDefaultAsync(e => e.Username == data.Username && e.Active)
						?? throw new NotFoundException($"No user for username {data.Username}");

			if (!VerifyPasswordHash(data.Password, user.PasswordHash, user.PasswordSalt))
				throw new ArgumentException("Invalid password.");

			return _jwtHelper.GenerateJwtToken(user);
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
	}
}