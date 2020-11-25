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
		DbSet<User> Users { get; }

		DbSet<DnsServer> DnsServers { get; }
		DbSet<WatchedDomain> WatchedDomains { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
	}
}