using FluentValidation;

namespace Requests.Application.Requests.V1.GetRequests
{
    public class GetRequestsQueryValidator : AbstractValidator<GetRequestsQuery>
    {
        public GetRequestsQueryValidator()
        {
        }
    }
}
