using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Requests.Domain.Models;

namespace Requests.Infrastructure.EntityConfiguration
{
    internal class RequestEntityConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.CreateDate)
                .IsRequired();

            builder
                .Property(x => x.RequestData)
                .IsRequired();

            builder
                .Property(x => x.AuthorName)
                .IsRequired();

            builder
                .Property(x => x.RequestTemplateId)
                .IsRequired();

            builder
                .Property(x => x.LdapUserId)
                .IsRequired();

            builder
                .HasOne(g => g.RequestTemplate)
                .WithMany(s => s.Requests)
                .HasForeignKey(s => s.RequestTemplateId);
        }
    }
}
