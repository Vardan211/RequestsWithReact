using FluentValidation;

namespace Requests.Application.Requests.V1.CreateRequest
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.RequestTemplateId)
                .NotEmpty()
                .WithMessage("Не указан идентификатор шаблона");

            RuleFor(x => x.RequestData)
                .NotEmpty()
                .WithMessage("Данные заявки отсутствтуют");

            RuleFor(x => x.PrimaryApprovers)
                .NotEmpty()
                .Must(x => x.Any() || x != null)
                .WithMessage("ID апруверов уровня approverGroups не указаны");
        }
    }
}
