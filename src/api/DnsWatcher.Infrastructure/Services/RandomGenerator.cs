using System;
using System.Security.Cryptography;
using DnsWatcher.Application.Helpers.Interfaces;

namespace DnsWatcher.Infrastructure.Services
{
	public class RandomGenerator : IRandomGenerator
	{
		public string GetRandomBase64ByteString(int numberOfBytes)
		{
			var container = new byte[numberOfBytes];
			using var generator = new RNGCryptoServiceProvider();
			generator.GetBytes(container);
			return Convert.ToBase64String(container);
		}
	}
}