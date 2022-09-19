using MediatR;

namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateById
{
    /// <summary>
    /// Запрос получения шаблона
    /// </summary>
    public class GetRequestTemplateByIdQuery : IRequest<GetRequestTemplateByIdResponse>
    {
        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        public int Id { get; set; }
    }
}
