using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Persistence.Context;
using DnsWatcher.Persistence.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Persistence.Helpers
{
	public class DnsWatcherDatabaseInitializer : IDatabaseInitializer
	{
		public async Task SeedAsync()
		{
			_logger.LogInformation("Starting database seed.");
			if (!await _context.Users.AnyAsync())
			{
				// seed users
			}

			if (!await _context.DnsServers.AnyAsync())
			{
				// seed dns servers
				await SeedDnsServers();
			}
			_logger.LogInformation("Finished database seed.");
		}

		#region construction

		private readonly DnsWatcherDbContext _context;
		private readonly ILogger<DnsWatcherDatabaseInitializer> _logger;
		private readonly IServiceProvider _serviceProvider;

		public DnsWatcherDatabaseInitializer(DnsWatcherDbContext context,
			ILogger<DnsWatcherDatabaseInitializer> logger, 
			IServiceProvider serviceProvider)
		{
			_context = context;
			_logger = logger;
			_serviceProvider = serviceProvider;
		}

		#endregion

		#region dns servers

		private async Task SeedDnsServers()
		{
			_logger.LogInformation("Seeding DNS servers.");
			var service = _serviceProvider.GetRequiredService<IDnsServersService>();
			await CreateDnsServer(service, "Google 1", "8.8.8.8", 53);
			await CreateDnsServer(service, "Google 2", "8.8.4.4", 53);
			await CreateDnsServer(service, "Cloudflare 1", "1.1.1.1", 53);
			await CreateDnsServer(service, "Cloudflare 2", "1.0.0.1", 53);
			await CreateDnsServer(service, "Proximus 1", "195.238.2.21", 53);
			await CreateDnsServer(service, "Proximus 2", "195.238.2.22", 53);
			_logger.LogInformation("DNS servers seeded.");
		}

		private async Task CreateDnsServer(IDnsServersService service, string name, string ip, int port)
		{
			var data = new CreateDnsServerData
			{
				Name = name,
				IpAddress = ip,
				Port = port
			};
			await service.CreateDnsServerAsync(data);
		}

		#endregion
	}
}