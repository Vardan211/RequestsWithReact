using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Requests.Application.Abstractions;
using Requests.Infrastructure.Options;
using Requests.Infrastructure.Repositories;

namespace Requests.Infrastructure.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей Infrastructure
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LdapOptions>(configuration.GetSection("LdapConnection"));

            services.AddRepositories();
            services.AddDbContext<RequestsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Db"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRequestTemplateRepository, RequestTemplateRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<ILdapUserRepository, LdapUserRepository>();
            return services;
        }
    }
}
