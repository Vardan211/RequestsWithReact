using MediatR;
using Requests.Application.Abstractions;

namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateList
{
    public class GetRequestTemplateListQueryHandler : IRequestHandler<GetRequestTemplateListQuery, GetRequestTemplateListResponse>
    {
        private readonly IRequestTemplateRepository _requestTemplateRepository;

        public GetRequestTemplateListQueryHandler(IRequestTemplateRepository requestTemplateRepository)
        {
            _requestTemplateRepository = requestTemplateRepository ?? throw new ArgumentNullException(nameof(requestTemplateRepository));
        }

        public async Task<GetRequestTemplateListResponse> Handle(GetRequestTemplateListQuery request, CancellationToken cancellationToken)
        {
            var requestTemplates = await _requestTemplateRepository.GetRequestTemplateListAsync(cancellationToken);

            GetRequestTemplateListResponse response = new()
            {
                Templates = requestTemplates
                    .Select(x => new GetRequestTemplate
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList()
                    .AsReadOnly(),
            };

            return response;
        }
    }
}
