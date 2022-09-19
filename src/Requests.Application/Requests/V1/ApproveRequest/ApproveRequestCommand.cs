using MediatR;

namespace Requests.Application.Requests.V1.ApproveRequest
{
    /// <summary>
    /// Команда согласования заявки
    /// </summary>
    public class ApproveRequestCommand : IRequest<Unit>
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
