using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Requests.Domain.Models;

namespace Requests.Infrastructure
{
    public class RequestsContext : DbContext
    {
        public RequestsContext(DbContextOptions<RequestsContext> options)
            : base(options)
        {
        }

        public DbSet<RequestTemplate> RequestTemplates { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<PrimaryApprove> PrimaryApprovers { get; set; }

        public DbSet<SecondaryApprove> SecondaryApprovers { get; set; }

        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
