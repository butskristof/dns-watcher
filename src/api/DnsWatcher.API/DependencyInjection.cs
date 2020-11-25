using DnsWatcher.API.Helpers;
using DnsWatcher.Common.Helpers.Interfaces;
using DnsWatcher.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DnsWatcher.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApi(this IServiceCollection services)
		{
			services.AddScoped<IAuthenticationInfo, ApiAuthenticationInfo>();
			services.AddHttpContextAccessor();

			services
				.AddHealthChecks()
				.AddDbContextCheck<DnsWatcherDbContext>();

			services
				.AddApplicationInsightsTelemetry();

			return services;
		}
	}
}