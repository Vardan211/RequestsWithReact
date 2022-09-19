using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Requests.Domain.Models;

namespace Requests.Infrastructure.EntityConfiguration
{
    internal class SettingsEntityConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder
                .ToTable("Settings");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Key)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasIndex(x => x.Key)
                .IsUnique(true);

            builder
                .Property(x => x.Value)
                .IsRequired();
        }
    }
}
