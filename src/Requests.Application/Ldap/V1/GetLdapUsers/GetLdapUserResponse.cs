namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    /// <summary>
    /// Ldap User
    /// </summary>
    public class GetLdapUserResponse
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
        /// Initializes a new instance of the <see cref="GetLdapUserResponse"/> class
        /// </summary>
        public GetLdapUserResponse()
        {
            Groups = Array.Empty<string>();
        }
    }
}
