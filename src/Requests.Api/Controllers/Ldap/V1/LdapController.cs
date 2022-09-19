using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requests.Application.Ldap.V1.GetLdapUserByIdentity;
using Requests.Application.Ldap.V1.GetLdapUsers;
using Swashbuckle.AspNetCore.Annotations;

namespace Requests.Api.Controllers.Ldap.V1
{
    /// <summary>
    /// Контроллер для Ldap
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class LdapController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр класса контроллера "Ldap" <see cref="LdapController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/></param>
        public LdapController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получение пользователей групп LDAP
        /// </summary>
        /// <param name="query"><see cref="GetLdapUsersQuery"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet("getUsers")]
        [SwaggerOperation(OperationId = nameof(GetLdapUsersAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetLdapUsersResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetLdapUsersAsync([FromQuery] GetLdapUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение пользователя LDAP
        /// </summary>
        /// <param name="query"><see cref="GetLdapUserByIdentityQuery"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet("getUser")]
        [SwaggerOperation(OperationId = nameof(GetLdapUserByIdentityAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetLdapUserByIdentityResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetLdapUserByIdentityAsync([FromQuery] GetLdapUserByIdentityQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
