using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Requests.Application.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей Application
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining(typeof(DiExtensions));
            });

            services.AddMediatR(typeof(DiExtensions));

            return services;
        }
    }
}
