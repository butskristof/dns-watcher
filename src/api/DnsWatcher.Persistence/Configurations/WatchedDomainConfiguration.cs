using DnsWatcher.Domain.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnsWatcher.Persistence.Configurations
{
	public class WatchedDomainConfiguration : IEntityTypeConfiguration<WatchedDomain>
	{
		public void Configure(EntityTypeBuilder<WatchedDomain> builder)
		{
			builder
				.Property(e => e.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(e => e.DomainName)
				.IsRequired();
		}
	}
}