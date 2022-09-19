namespace Requests.Domain.Models
{
    /// <summary>
    /// Пользователь LDAP
    /// </summary>
    public class LdapUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Группы пользователя
        /// </summary>
        public IReadOnlyCollection<string> Groups { get; set; }
    }
}
