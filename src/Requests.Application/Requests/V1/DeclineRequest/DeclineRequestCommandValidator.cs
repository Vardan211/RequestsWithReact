using FluentValidation;

namespace Requests.Application.Requests.V1.DeclineRequest
{
    public class DeclineRequestCommandValidator : AbstractValidator<DeclineRequestCommand>
    {
        public DeclineRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Не указан идентификатор заявки");

            RuleFor(x => x.LdapUserId)
               .NotEmpty()
               .WithMessage("Не указан идентификатор согласующего пользователя");
        }
    }
}
