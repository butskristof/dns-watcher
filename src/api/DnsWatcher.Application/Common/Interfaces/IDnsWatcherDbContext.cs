using System.Threading;
using System.Threading.Tasks;
using DnsWatcher.Domain.Entities.Domains;
using DnsWatcher.Domain.Entities.Identity;
using DnsWatcher.Domain.Entities.Servers;
using Microsoft.EntityFrameworkCore;

namespace DnsWatcher.Application.Common.Interfaces
{
	public interface IDnsWatcherDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<RefreshToken> RefreshTokens { get; set; }

		DbSet<WatchedDomain> WatchedDomains { get; set; }
		DbSet<WatchedRecord> WatchedRecords { get; set; }
		DbSet<DnsServer> DnsServers { get; set; }
		DbSet<RecordServerResult> RecordServerResults { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
	}
}