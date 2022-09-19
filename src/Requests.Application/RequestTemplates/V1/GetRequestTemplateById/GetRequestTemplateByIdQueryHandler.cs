using MediatR;
using Requests.Application.Abstractions;

namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateById
{
    public class GetRequestTemplateByIdQueryHandler : IRequestHandler<GetRequestTemplateByIdQuery, GetRequestTemplateByIdResponse>
    {
        private readonly IRequestTemplateRepository _requestTemplateRepository;

        public GetRequestTemplateByIdQueryHandler(IRequestTemplateRepository requestTemplateRepository)
        {
            _requestTemplateRepository = requestTemplateRepository ?? throw new ArgumentNullException(nameof(requestTemplateRepository));
        }

        public async Task<GetRequestTemplateByIdResponse> Handle(GetRequestTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var requestTemplate = await _requestTemplateRepository.GetRequestTemplateByIdAsync(request.Id, cancellationToken);

            GetRequestTemplateByIdResponse response = new()
            {
                Id = requestTemplate.Id,
                Name = requestTemplate.Name,
                Template = requestTemplate.Template,
            };

            return response;
        }
    }
}
