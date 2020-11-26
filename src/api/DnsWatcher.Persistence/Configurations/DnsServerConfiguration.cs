using DnsWatcher.Domain.Entities.Servers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnsWatcher.Persistence.Configurations
{
	public class DnsServerConfiguration : IEntityTypeConfiguration<DnsServer>
	{
		public void Configure(EntityTypeBuilder<DnsServer> builder)
		{
			builder
				.Property(e => e.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(e => e.Name)
				.IsRequired();

			builder
				.Property(e => e.IpAddress)
				.IsRequired();

			builder
				.Property(e => e.Port)
				.IsRequired();
		}
	}
}