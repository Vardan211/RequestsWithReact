using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Requests.Domain.Models;

namespace Requests.Infrastructure.EntityConfiguration
{
    internal class SecondaryApproveEntityConfiguration : IEntityTypeConfiguration<SecondaryApprove>
    {
        public void Configure(EntityTypeBuilder<SecondaryApprove> builder)
        {
            builder
                .ToTable("SecondaryApprovers");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.LdapUserId)
                .IsRequired();

            builder
                .Property(x => x.RequestId)
                .IsRequired();

            builder
                .Property(x => x.GroupName)
                .IsRequired();

            builder
                .HasOne(g => g.Request)
                .WithMany(s => s.SecondaryApprovers)
                .HasForeignKey(s => s.RequestId);
        }
    }
}
