using Microsoft.EntityFrameworkCore;
using Requests.Infrastructure;

namespace Requests.Api.Extensions
{
    internal static class MigratesExtensions
    {
        public static async Task AutoMigrate(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<RequestsContext>();
                await dataContext.Database.MigrateAsync();
            }
        }
    }
}
