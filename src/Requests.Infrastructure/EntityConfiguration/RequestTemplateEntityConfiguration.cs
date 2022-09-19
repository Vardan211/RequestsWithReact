using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Requests.Domain.Models;

namespace Requests.Infrastructure.EntityConfiguration
{
    internal class RequestTemplateEntityConfiguration : IEntityTypeConfiguration<RequestTemplate>
    {
        public void Configure(EntityTypeBuilder<RequestTemplate> builder)
        {
            builder.ToTable(nameof(RequestTemplate));

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(x => x.Template)
                .IsRequired();
        }
    }
}
