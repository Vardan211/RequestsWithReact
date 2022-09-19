using MediatR;
using Requests.Application.Abstractions;

namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    public class GetLdapUsersQueryHandler : IRequestHandler<GetLdapUsersQuery, GetLdapUsersResponse>
    {
        private readonly ILdapUserRepository _ldapUserRepository;

        public GetLdapUsersQueryHandler(ILdapUserRepository ldapUserRepository)
        {
            _ldapUserRepository = ldapUserRepository ?? throw new ArgumentNullException(nameof(ldapUserRepository));
        }

        public Task<GetLdapUsersResponse> Handle(GetLdapUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetLdapUsersResponse();

            var ldapUsers = _ldapUserRepository.GetUsersByGroups(request.GroupNames.ToArray(), cancellationToken);

            response.Groups = request.GroupNames
                .Select(name => new LdapGroupResponse
                {
                    GroupName = name,
                })
                .ToList();

            foreach (var (group, user) in response.Groups
                .SelectMany(group => ldapUsers
                    .SelectMany(ldapUser => ldapUser.Groups
                        .Where(usersGroup => group.GroupName == usersGroup)
                        .Select(usersGroup => (group, ldapUser)))))
            {
                var ldapUser = new GetLdapUserResponse()
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Groups = user.Groups.ToArray(),
                };

                if (!group.Users.Any(u => u.Id.Equals(ldapUser.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    group.Users.Add(ldapUser);
                }
            }

            return Task.FromResult(response);
        }
    }
}
