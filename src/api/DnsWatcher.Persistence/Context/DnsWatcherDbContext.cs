using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DnsWatcher.Application.Common.Interfaces;
using DnsWatcher.Common.Constants;
using DnsWatcher.Common.Extensions;
using DnsWatcher.Common.Helpers.Interfaces;
using DnsWatcher.Domain.Common;
using DnsWatcher.Domain.Entities.Domains;
using DnsWatcher.Domain.Entities.Identity;
using DnsWatcher.Domain.Entities.Servers;
using DnsWatcher.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DnsWatcher.Persistence.Context
{
	public class DnsWatcherDbContext : DbContext, IDnsWatcherDbContext
	{
		#region auditing

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			var userId = _authenticationInfo.UserId?.ToSafeString()
						?? "Unregistered";
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				var timestamp = _dateTime.Now;
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = userId;
						entry.Entity.CreatedOn = timestamp;
						entry.Entity.ModifiedBy = userId;
						entry.Entity.SetModifiedOnForContext(timestamp);
						break;
					case EntityState.Modified:
						entry.Entity.ModifiedBy = userId;
						entry.Entity.SetModifiedOnForContext(timestamp);
						break;
					case EntityState.Deleted:
						entry.Entity.DeletedBy = userId;
						entry.Entity.DeletedOn = _dateTime.Now;
						entry.State = EntityState.Modified;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		#endregion

		#region configuration

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			// https://stackoverflow.com/a/50852517
			foreach (var property in builder.Model.GetEntityTypes()
				.SelectMany(t => t.GetProperties())
				.Where(p => p.ClrType == typeof(string)))
				if (property.GetMaxLength() == null) // do not override if explicitly set
					property.SetMaxLength(PersistenceConstants.DefaultMaxStringLength);

			base.OnModelCreating(builder);

			builder
				.ApplyGlobalFilters<ISoftDeletableEntity>(e => !e.DeletedOn.HasValue);
		}

		#endregion

		#region construction

		private readonly IAuthenticationInfo _authenticationInfo;
		private readonly IDateTime _dateTime;

		public DnsWatcherDbContext(DbContextOptions options,
			IAuthenticationInfo authenticationInfo,
			IDateTime dateTime)
			: base(options)
		{
			_authenticationInfo = authenticationInfo;
			_dateTime = dateTime;
		}

		#endregion

		#region entities

		public DbSet<User> Users { get; set; }
		public DbSet<DnsServer> DnsServers { get; set; }
		public DbSet<WatchedDomain> WatchedDomains { get; set; }
		public DbSet<WatchedRecord> WatchedRecords { get; set; }
		public DbSet<RecordServerResult> RecordServerResults { get; set; }

		#endregion
	}
}