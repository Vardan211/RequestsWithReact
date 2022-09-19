using MediatR;

namespace Requests.Application.Requests.V1.GetRequest
{
    /// <summary>
    /// Запрос заявки
    /// </summary>
    public class GetRequestQuery : IRequest<GetRequestResponse>
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public int RequestId { get; set; }
    }
}
