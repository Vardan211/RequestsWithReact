using MediatR;

namespace Requests.Application.Requests.V1.GetRequests
{
    /// <summary>
    /// Запрос заявкок для пользователя LDAP
    /// </summary>
    public class GetRequestsQuery : IRequest<GetRequestsResponse>
    {
        /// <summary>
        /// ID пользователя LDAP
        /// </summary>
        public string LdapUserId { get; set; }
    }
}
