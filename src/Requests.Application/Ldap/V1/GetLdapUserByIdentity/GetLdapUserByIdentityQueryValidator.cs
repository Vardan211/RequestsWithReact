using FluentValidation;

namespace Requests.Application.Ldap.V1.GetLdapUserByIdentity
{
    public class GetLdapUserByIdentityQueryValidator : AbstractValidator<GetLdapUserByIdentityQuery>
    {
        public GetLdapUserByIdentityQueryValidator()
        {
            RuleFor(x => x.UserIdentity)
               .NotEmpty()
               .WithMessage("Не указан Guid пользователя");
        }
    }
}
