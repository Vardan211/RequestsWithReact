using FluentValidation;

namespace Requests.Application.Ldap.V1.GetLdapUsers
{
    public class GetLdapUsersQueryValidator : AbstractValidator<GetLdapUsersQuery>
    {
        public GetLdapUsersQueryValidator()
        {
            RuleFor(x => x.GroupNames)
               .NotEmpty()
               .WithMessage("Не указаны названия групп LDAP");
        }
    }
}
