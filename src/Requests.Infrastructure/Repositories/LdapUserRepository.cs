#pragma warning disable CA1416 // Validate platform compatibility
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Options;
using Requests.Application.Abstractions;
using Requests.Domain.Models;
using Requests.Infrastructure.Exceptions;
using Requests.Infrastructure.Options;

namespace Requests.Infrastructure.Repositories
{
    /// <inheritdoc/>
    internal class LdapUserRepository : ILdapUserRepository
    {
        private readonly LdapOptions _ldapOptions;

        public LdapUserRepository(IOptions<LdapOptions> ldapOptions)
        {
            _ldapOptions = ldapOptions?.Value ?? throw new ArgumentNullException(nameof(ldapOptions));
        }

        /// <inheritdoc/>
        public LdapUser GetUser(string uuid, CancellationToken token = default)
        {
            using var ctx = new PrincipalContext(ContextType.Domain, _ldapOptions.Domain, _ldapOptions.Username, _ldapOptions.Password);

            var ldapUser = UserPrincipal.FindByIdentity(ctx, uuid);

            if (ldapUser == null)
            {
                NotFoundException.Throw(uuid);
            }

            var ldapUserGroups = ldapUser.GetGroups();

            var user = new LdapUser()
            {
                UserName = ldapUser.Name,
                Id = ldapUser.Guid.ToString(),
                Groups = ldapUserGroups?.Select(x => x.Name)?.ToList() ?? new List<string>(),
            };

            return user;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<LdapUser> GetUsersByGroups(string[] groups, CancellationToken token = default)
        {
            using var ctx = new PrincipalContext(ContextType.Domain, _ldapOptions.Domain, _ldapOptions.Username, _ldapOptions.Password);

            var result = new List<LdapUser>();

            foreach (var group in groups)
            {
                var ldapGroup = GroupPrincipal.FindByIdentity(ctx, group);

                if (ldapGroup == null)
                {
                    continue;
                }

                foreach (var ldapUser in ldapGroup.GetMembers())
                {
                    var ldapUserGroups = ldapUser.GetGroups();

                    var user = new LdapUser()
                    {
                        UserName = ldapUser.Name,
                        Id = ldapUser.Guid.ToString(),
                        Groups = ldapUserGroups?.Select(x => x.Name)?.ToList() ?? new List<string>(),
                    };

                    result.Add(user);
                }
            }

            return result;
        }
    }
}
#pragma warning restore CA1416 // Validate platform compatibility
