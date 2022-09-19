using MediatR;

namespace Requests.Application.Requests.V1.DeclineRequest
{
    /// <summary>
    /// Команда отклонение заявки
    /// </summary>
    public class DeclineRequestCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор согласуещего
        /// </summary>
        public string LdapUserId { get; set; }
    }
}
