using System.Threading.Tasks;
using DnsWatcher.Persistence.Context;
using DnsWatcher.Persistence.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
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
			_logger.LogInformation("Finished database seed.");
		}

		#region construction

		private readonly DnsWatcherDbContext _context;
		private readonly ILogger<DnsWatcherDatabaseInitializer> _logger;
		// private readonly IServiceProvider _serviceProvider;

		public DnsWatcherDatabaseInitializer(DnsWatcherDbContext context,
			ILogger<DnsWatcherDatabaseInitializer> logger)
		{
			_context = context;
			_logger = logger;
		}

		#endregion
	}
}