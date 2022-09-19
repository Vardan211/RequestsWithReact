using FluentValidation;

namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateById
{
    public class GetRequestTemplateByIdQueryValidator : AbstractValidator<GetRequestTemplateByIdQuery>
    {
        public GetRequestTemplateByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Не указан идентификатор шаблона");
        }
    }
}
