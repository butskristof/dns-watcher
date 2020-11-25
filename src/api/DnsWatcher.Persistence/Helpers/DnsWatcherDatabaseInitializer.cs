using System;
using System.Threading.Tasks;
using DnsWatcher.Persistence.Context;
using DnsWatcher.Persistence.Helpers.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.Persistence.Helpers
{
	public class DnsWatcherDatabaseInitializer : IDatabaseInitializer
	{
		public Task SeedAsync()
		{
			_logger.LogInformation("Starting database seed.");
			_logger.LogInformation("Finished database seed.");
			return Task.CompletedTask;
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
	}
}