using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Requests.Domain.Models;

namespace Requests.Infrastructure.EntityConfiguration
{
    internal class PrimaryApproveEntityConfiguration : IEntityTypeConfiguration<PrimaryApprove>
    {
        public void Configure(EntityTypeBuilder<PrimaryApprove> builder)
        {
            builder
                .ToTable("PrimaryApprovers");

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
                .WithMany(s => s.PrimaryApprovers)
                .HasForeignKey(s => s.RequestId);
        }
    }
}
