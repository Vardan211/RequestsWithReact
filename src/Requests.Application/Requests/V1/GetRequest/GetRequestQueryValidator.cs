using FluentValidation;

namespace Requests.Application.Requests.V1.GetRequest
{
    public class GetRequestQueryValidator : AbstractValidator<GetRequestQuery>
    {
        public GetRequestQueryValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty()
                .WithMessage("Не указан идентификатор заявки");
        }
    }
}
