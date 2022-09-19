using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requests.Application.Requests.V1.ApproveRequest;
using Requests.Application.Requests.V1.CreateRequest;
using Requests.Application.Requests.V1.DeclineRequest;
using Requests.Application.Requests.V1.GetRequest;
using Requests.Application.Requests.V1.GetRequests;
using Requests.Application.Requests.V1.RecallRequest;
using Swashbuckle.AspNetCore.Annotations;

namespace Requests.Api.Controllers.Requests.V1
{
    /// <summary>
    /// Контроллер для заявок
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр класса контроллера "Заявки" <see cref="RequestTemplatesController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/></param>
        public RequestsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Создание заявки
        /// </summary>
        /// <param name="request"><see cref="CreateRequestCommand"/></param>
        /// <param name="ldapUserId">Идентификатор пользователя LDAP</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = nameof(CreateRequestAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CreateRequestResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateRequestAsync(
            [FromBody] CreateRequestCommand request,
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            request.LdapUserId = ldapUserId;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Получение заявок
        /// </summary>
        /// <param name="request"><see cref="GetRequestsQuery"/></param>
        /// <param name="ldapUserId">Идентификатор пользователя LDAP</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(GetRequestsAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetRequestsResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRequestsAsync(
            [FromQuery] GetRequestsQuery request,
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            request.LdapUserId = ldapUserId;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Получение заявки
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(GetRequestAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetRequestResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRequestAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRequestQuery { RequestId = id }, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Подтвердить заявку
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="ldapUserId">Идентификатор пользователя в LDAP</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost("{id}/approve")]
        [SwaggerOperation(OperationId = nameof(ApproveAsync))]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ApproveAsync(
            [FromRoute] int id,
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new ApproveRequestCommand { Id = id, LdapUserId = ldapUserId }, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Отменить подтверждение заявки
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="ldapUserId">Идентификатор пользователя в LDAP</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost("{id}/decline")]
        [SwaggerOperation(OperationId = nameof(DeclineAsync))]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeclineAsync(
            [FromRoute] int id,
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeclineRequestCommand { Id = id, LdapUserId = ldapUserId }, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Отозвать заявку
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="ldapUserId">Идентификатор пользователя в LDAP</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost("{id}/recall")]
        [SwaggerOperation(OperationId = nameof(RecallAsync))]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RecallAsync(
            [FromRoute] int id,
            [FromHeader(Name = "LdapUserId")] string ldapUserId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RecallRequestCommand { Id = id, LdapUserId = ldapUserId }, cancellationToken);
            return Ok();
        }
    }
}
