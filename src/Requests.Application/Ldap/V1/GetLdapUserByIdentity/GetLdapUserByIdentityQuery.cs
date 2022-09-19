using MediatR;

namespace Requests.Application.Ldap.V1.GetLdapUserByIdentity
{
    /// <summary>
    /// Запрос получения пользователя LDAP
    /// </summary>
    public class GetLdapUserByIdentityQuery : IRequest<GetLdapUserByIdentityResponse>
    {
        /// <summary>
        /// Identity пользователя (UUID | Email)
        /// </summary>
        public string UserIdentity { get; set; }
    }
}
