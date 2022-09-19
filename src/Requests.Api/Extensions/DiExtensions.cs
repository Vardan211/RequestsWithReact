using Requests.Application.Extensions;
using Requests.Domain.Extensions;
using Requests.Infrastructure.Extensions;

namespace Requests.Api.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей Layers
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection RegisterLayers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainLayer();
            services.AddApplicationLayer();
            services.AddInfrastructureLayer(configuration);

            return services;
        }

        /// <summary>
        /// Подключение настроек
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configuration"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
