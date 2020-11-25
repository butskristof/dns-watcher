using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Common.Helpers.Interfaces;
using DnsWatcher.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DnsWatcher.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services
				.AddTransient<IDateTime, DateTimeService>();
			services
				.AddScoped<IEncryptionHelper, StringCipher>();

			return services;
		}
	}
}