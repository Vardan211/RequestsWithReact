using FluentValidation;

namespace Requests.Application.Requests.V1.ApproveRequest
{
    public class ApproveRequestCommandValidator : AbstractValidator<ApproveRequestCommand>
    {
        public ApproveRequestCommandValidator()
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
