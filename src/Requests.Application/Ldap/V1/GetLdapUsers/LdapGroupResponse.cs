namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    /// <summary>
    /// Groups of Ldap users
    /// </summary>
    public class LdapGroupResponse
    {
        /// <summary>
        /// Имя группы
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Пользователи группы
        /// </summary>
        public ICollection<GetLdapUserResponse> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapGroupResponse"/> class
        /// </summary>
        public LdapGroupResponse()
        {
            Users = new List<GetLdapUserResponse>();
        }
    }
}
