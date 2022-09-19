using MediatR;

namespace Requests.Application.Settings.V1.GetTabs
{
    /// <summary>
    /// Запрос вкладок для пользователя
    /// </summary>
    public class GetTabsQuery : IRequest<string[]>
    {
        /// <summary>
        /// ID пользователя LDAP
        /// </summary>
        public string LdapUserId { get; set; }
    }
}
