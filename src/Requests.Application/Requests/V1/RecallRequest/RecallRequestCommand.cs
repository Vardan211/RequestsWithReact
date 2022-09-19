using MediatR;

namespace Requests.Application.Requests.V1.RecallRequest
{
    /// <summary>
    /// Команда удаления заявки
    /// </summary>
    public class RecallRequestCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор автора заявки
        /// </summary>
        public string LdapUserId { get; set; }
    }
}
