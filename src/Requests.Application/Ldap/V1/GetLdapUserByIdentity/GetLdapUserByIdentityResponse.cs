namespace Requests.Application.Ldap.V1.GetLdapUserByIdentity
{
    public class GetLdapUserByIdentityResponse
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
        /// Initializes a new instance of the <see cref="GetLdapUserByIdentityResponse"/> class
        /// </summary>
        public GetLdapUserByIdentityResponse()
        {
            Groups = Array.Empty<string>();
        }
    }
}
