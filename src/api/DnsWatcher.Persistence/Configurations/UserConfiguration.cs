using DnsWatcher.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnsWatcher.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.Property(e => e.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(e => e.Username)
				.IsRequired();

			builder
				.Property(e => e.Active)
				.IsRequired()
				.HasDefaultValue(false);

			builder
				.Property(e => e.PasswordHash)
				.HasMaxLength(64)
				.IsFixedLength()
				.IsRequired();

			builder
				.Property(e => e.PasswordSalt)
				.HasMaxLength(128)
				.IsFixedLength()
				.IsRequired();
		}
	}
}