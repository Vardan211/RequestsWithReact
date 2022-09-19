using MediatR;

namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    /// <summary>
    /// Запрос поиска пользователей LDAP
    /// </summary>
    public class GetLdapUsersQuery : IRequest<GetLdapUsersResponse>
    {
        /// <summary>
        /// Названия групп LDAP
        /// </summary>
        public string[] GroupNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLdapUsersQuery"/> class
        /// </summary>
        public GetLdapUsersQuery()
        {
            GroupNames = Array.Empty<string>();
        }
    }
}
