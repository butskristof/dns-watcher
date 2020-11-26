using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Common.Constants;
using DnsWatcher.Persistence.Context;
using DnsWatcher.Persistence.Helpers;
using DnsWatcher.Persistence.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DnsWatcher.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services,
			IConfiguration configuration)
		{
			services
				.AddDbContext<DnsWatcherDbContext>(opt =>
					opt.UseNpgsql(configuration
							.GetConnectionString(PersistenceConstants.DnsWatcherDb),
						b => b
							.MigrationsAssembly(typeof(DnsWatcherDbContext).Assembly.FullName)));

			services
				.AddScoped<IDnsWatcherDbContext, DnsWatcherDbContext>();
			services
				.AddScoped<IDatabaseInitializer, DnsWatcherDatabaseInitializer>();

			return services;
		}
	}
}