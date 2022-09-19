using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requests.Application.Settings.V1.GetTabs;
using Swashbuckle.AspNetCore.Annotations;

namespace Requests.Api.Controllers.Settings.V1
{
    /// <summary>
    /// Контроллер для Настроек
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр класса контроллера "Шаблоны Заявок" <see cref="RequestTemplatesController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/></param>
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получение вкладок
        /// </summary>
        /// <param name="ldapUserId">Идентификатор пользователя Ldap</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet("tabs")]
        [SwaggerOperation(OperationId = nameof(GetTabsAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetTabsAsync(
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetTabsQuery { LdapUserId = ldapUserId }, cancellationToken);
            return Ok(response);
        }
    }
}
