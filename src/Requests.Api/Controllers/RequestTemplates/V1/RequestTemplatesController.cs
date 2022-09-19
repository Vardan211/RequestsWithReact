using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requests.Application.RequestTemplates.V1.GetRequestTemplateById;
using Requests.Application.RequestTemplates.V1.GetRequestTemplateList;
using Swashbuckle.AspNetCore.Annotations;

namespace Requests.Api.Controllers.RequestTemplatesController.V1
{
    /// <summary>
    /// Контроллер для шаблонов заявок
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class RequestTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр класса контроллера "Шаблоны Заявок" <see cref="RequestTemplatesController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/></param>
        public RequestTemplatesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получение шаблона заявок по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="token"></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(GetRequestTemplateByIdAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetRequestTemplateByIdResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRequestTemplateByIdAsync(int id, CancellationToken token = default)
        {
            var response = await _mediator.Send(new GetRequestTemplateByIdQuery { Id = id }, token);
            return Ok(response);
        }

        /// <summary>
        /// Получение списка шаблонов заявок
        /// </summary>
        /// <param name="query"><see cref="GetRequestTemplateListQuery"/></param>
        /// <param name="token"></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(GetRequestTemplateListAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetRequestTemplateListResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRequestTemplateListAsync([FromRoute] GetRequestTemplateListQuery query, CancellationToken token = default)
        {
            var response = await _mediator.Send(query, token);
            return Ok(response);
        }
    }
}
