using System.Net.Mime;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;
using Requests.Application.Exceptions;
using Requests.Domain.Exceptions;
using Requests.Infrastructure.Exceptions;

namespace Requests.Api.Middlewares
{
    /// <summary>
    /// Middleware обработки исключений
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/></param>
        /// <returns><see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // todo: log error
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = exception switch
            {
                TimeoutException _ => StatusCodes.Status504GatewayTimeout,
                AuthenticationException _ => StatusCodes.Status401Unauthorized,
                NotFoundException _ => StatusCodes.Status404NotFound,
                AppException _ => StatusCodes.Status500InternalServerError,
                DomainException _ => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };

            return httpContext.Response.WriteAsJsonAsync(
                new ErrorResponse
                {
                    Message = exception.Message,
                },
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }
    }
}
