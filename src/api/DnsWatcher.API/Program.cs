using System;
using System.Threading.Tasks;
using DnsWatcher.Persistence.Context;
using DnsWatcher.Persistence.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			try
			{
				var ctx = services.GetRequiredService<DnsWatcherDbContext>();
				await ctx.Database.MigrateAsync();

				var initializer = services.GetRequiredService<IDatabaseInitializer>();
				await initializer.SeedAsync();
			}
			catch (Exception e)
			{
				var logger = services.GetRequiredService<ILogger<Program>>();
				logger.LogError(e, "An error occurred while migrating or seeding the database.");
				throw;
			}

			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}
	}
}