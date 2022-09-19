using MediatR;

namespace Requests.Application.Requests.V1.CreateRequest
{
    /// <summary>
    /// Команда создания заявки
    /// </summary>
    public class CreateRequestCommand : IRequest<CreateRequestResponse>
    {
        /// <summary>
        /// Идентификтаор шаблона
        /// </summary>
        public int RequestTemplateId { get; set; }

        /// <summary>
        /// JSON с данными заявки
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// ID автора заявки LDAP
        /// </summary>
        public string LdapUserId { get; set; }

        /// <summary>
        /// ID апруверов уровня approverGroups
        /// </summary>
        public CreateRequestCommandApprover[] PrimaryApprovers { get; set; }

        /// <summary>
        /// ID апруверов уровня solutionGroups
        /// </summary>
        public CreateRequestCommandApprover[] SecondaryApprovers { get; set; }
    }
}
