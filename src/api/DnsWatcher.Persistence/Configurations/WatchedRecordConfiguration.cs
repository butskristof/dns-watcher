using DnsWatcher.Domain.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnsWatcher.Persistence.Configurations
{
	public class WatchedRecordConfiguration : IEntityTypeConfiguration<WatchedRecord>
	{
		public void Configure(EntityTypeBuilder<WatchedRecord> builder)
		{
			builder
				.Property(e => e.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(e => e.RecordType)
				.HasConversion<string>();
		}
	}
}