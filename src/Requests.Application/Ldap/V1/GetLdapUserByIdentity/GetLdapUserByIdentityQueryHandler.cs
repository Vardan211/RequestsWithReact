using MediatR;
using Requests.Application.Abstractions;
using Requests.Domain.Models;

namespace Requests.Application.Ldap.V1.GetLdapUserByIdentity
{
    public class GetLdapUserByIdentityQueryHandler : IRequestHandler<GetLdapUserByIdentityQuery, GetLdapUserByIdentityResponse>
    {
        private readonly ILdapUserRepository _ldapUserRepository;

        public GetLdapUserByIdentityQueryHandler(ILdapUserRepository ldapUserRepository)
        {
            _ldapUserRepository = ldapUserRepository ?? throw new ArgumentNullException(nameof(ldapUserRepository));
        }

        public Task<GetLdapUserByIdentityResponse> Handle(GetLdapUserByIdentityQuery request, CancellationToken cancellationToken)
        {
            var user = _ldapUserRepository.GetUser(request.UserIdentity, cancellationToken);

            return Task.FromResult(CreateResponse(user));
        }

        private GetLdapUserByIdentityResponse CreateResponse(LdapUser user)
        {
            var response = new GetLdapUserByIdentityResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Groups = user.Groups.ToArray(),
            };
            return response;
        }
    }
}
