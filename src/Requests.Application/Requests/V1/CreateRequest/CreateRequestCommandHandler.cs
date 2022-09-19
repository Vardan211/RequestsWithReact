using MediatR;
using Requests.Application.Abstractions;
using Requests.Domain.Models;

namespace Requests.Application.Requests.V1.CreateRequest
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, CreateRequestResponse>
    {
        private readonly IRequestRepository _requestRepository;

        public CreateRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }

        public async Task<CreateRequestResponse> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            Request requestModel = new()
            {
                RequestData = request.RequestData,
                RequestTemplateId = request.RequestTemplateId,
                LdapUserId = Guid.Parse(request.LdapUserId),
                PrimaryApprovers = request.PrimaryApprovers
                    .Select(x => new PrimaryApprove
                    {
                        LdapUserId = Guid.Parse(x.LdapUserId),
                        GroupName = x.GroupName,
                        Approved = null,
                    })
                    .ToList(),
                SecondaryApprovers = request.SecondaryApprovers
                    ?.Select(x => new SecondaryApprove
                    {
                        LdapUserId = Guid.Parse(x.LdapUserId),
                        GroupName = x.GroupName,
                        Approved = null,
                    })
                    .ToList(),
            };

            var requestId = await _requestRepository.CreateAsync(requestModel, cancellationToken);

            CreateRequestResponse response = new()
            {
                RequestId = requestId,
            };

            return response;
        }
    }
}
