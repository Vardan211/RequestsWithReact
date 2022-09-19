namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    /// <summary>
    /// Groups of Ldap users
    /// </summary>
    public class GetLdapUsersResponse
    {
        /// <summary>
        /// Группы
        /// </summary>
        public ICollection<LdapGroupResponse> Groups { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLdapUsersResponse"/> class
        /// </summary>
        public GetLdapUsersResponse()
        {
            Groups = new List<LdapGroupResponse>();
        }
    }
}
