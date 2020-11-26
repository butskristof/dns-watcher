using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DnsWatcher.Common.Configuration;
using DnsWatcher.Common.Exceptions;
using DnsWatcher.Common.Helpers.Interfaces;

namespace DnsWatcher.Infrastructure.Services
{
	internal class StringCipher : IEncryptionHelper
	{
		private readonly IEncryptionOptions _options;

		public StringCipher(IEncryptionOptions options)
		{
			_options = options;
		}

		public string Encrypt(string value)
		{
			var iv = new byte[16];

			using var aes = Aes.Create()
							?? throw new InfrastructureException("Failed to create Aes object.");
			aes.Key = Encoding.UTF8.GetBytes(_options.Key);
			aes.IV = iv;

			var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			using var memoryStream = new MemoryStream();
			using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			using (var streamWriter = new StreamWriter(cryptoStream))
			{
				streamWriter.Write(value);
			}

			var array = memoryStream.ToArray();

			return Convert.ToBase64String(array);
		}

		public string Decrypt(string cipherText)
		{
			var iv = new byte[16];
			var buffer = Convert.FromBase64String(cipherText);

			using var aes = Aes.Create()
							?? throw new InfrastructureException("Failed to create Aes object.");
			aes.Key = Encoding.UTF8.GetBytes(_options.Key);
			aes.IV = iv;
			var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

			using var memoryStream = new MemoryStream(buffer);
			using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			using var streamReader = new StreamReader(cryptoStream);
			return streamReader.ReadToEnd();
		}
	}
}