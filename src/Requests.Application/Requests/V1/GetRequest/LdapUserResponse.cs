namespace Requests.Application.Requests.V1.GetRequest
{
    /// <summary>
    /// Ldap User в ответе на запрос заявки
    /// </summary>
    public class LdapUserResponse
    {
        /// <summary>
        /// ID пользователя LDAP
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ФИО пользователя LDAP
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Группы пользователя LDAP
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// Подтверждение
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapUserResponse"/> class
        /// </summary>
        public LdapUserResponse()
        {
            Groups = Array.Empty<string>();
        }
    }
}
