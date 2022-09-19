using MediatR;
using Requests.Application.Abstractions;
using Requests.Application.Extensions;

namespace Requests.Application.Requests.V1.GetRequest
{
    public class GetRequestQueryHandler : IRequestHandler<GetRequestQuery, GetRequestResponse>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ILdapUserRepository _ldapUserRepository;

        public GetRequestQueryHandler(IRequestRepository requestRepository, ILdapUserRepository ldapUserRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
            _ldapUserRepository = ldapUserRepository ?? throw new ArgumentNullException(nameof(ldapUserRepository));
        }

        public async Task<GetRequestResponse> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var requestId = await _requestRepository.GetByIdAsync(request.RequestId, cancellationToken);

            GetRequestResponse response = new()
            {
                Id = requestId.Id,
                RequestData = requestId.RequestData,
                LdapUserId = requestId.LdapUserId.ToString(),
                AuthorName = requestId.AuthorName,
                IsApproved = requestId.CheckApprove(),
                PrimaryApproverGroupLdapUsers = requestId.PrimaryApprovers
                        .Where(x => x.RequestId == requestId.Id)
                        .Select(x =>
                        {
                            var approver = _ldapUserRepository.GetUser(x.LdapUserId.ToString());
                            return new LdapUserResponse
                            {
                                Id = x.LdapUserId.ToString(),
                                UserName = approver.UserName,
                                Groups = approver.Groups.ToArray(),
                                IsApproved = x.Approved,
                            };
                        })
                        .ToArray(),
                SecondaryApproverGroupLdapUsers = requestId.SecondaryApprovers
                        .Where(x => x.RequestId == requestId.Id)
                        .Select(x =>
                        {
                            var approver = _ldapUserRepository.GetUser(x.LdapUserId.ToString());
                            return new LdapUserResponse
                            {
                                Id = x.LdapUserId.ToString(),
                                UserName = approver.UserName,
                                Groups = approver.Groups.ToArray(),
                                IsApproved = x.Approved,
                            };
                        })
                        .ToArray(),
            };
            return response;
        }
    }
}
