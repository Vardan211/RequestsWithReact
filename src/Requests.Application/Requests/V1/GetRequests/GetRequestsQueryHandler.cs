using System.Linq;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests.Application.Abstractions;
using Requests.Application.Exceptions;
using Requests.Application.Extensions;
using Requests.Domain.Constants;
using Requests.Domain.Models;

namespace Requests.Application.Requests.V1.GetRequests
{
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, GetRequestsResponse>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ISettingRepository _settingRepository;

        public GetRequestsQueryHandler(IRequestRepository requestRepository, ISettingRepository settingRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
            _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
        }

        public async Task<GetRequestsResponse> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.LdapUserId, out var userId))
            {
                throw new AppException("Не удалось распознать LdapUserId как Guid");
            }

            var requests = await _requestRepository.GetRequestsByUserIdAsync(userId, cancellationToken);
            var userSetting = await _settingRepository.GetSettingByKeyAsync(SettingsNames.RequestViewer, cancellationToken);

            var response = new GetRequestsResponse
            {
                Requests = requests
                     .Where(x => x.LdapUserId == userId)
                     .OrderByDescending(x => x.CreateDate)
                     .Select(x => new RequestResponse
                     {
                         Id = x.Id,
                         IsApproved = x.CheckApprove(),
                         Name = x.RequestTemplate.Name,
                     })
                     .ToArray(),
            };

            foreach (var requestForApprove in requests)
            {
                var requestDataJson = (JObject)JsonConvert.DeserializeObject(requestForApprove.RequestData);
                var primaryGroups = requestDataJson.SelectToken("approverGroups")?.ToObject<string[]>();

                var temp = new List<RequestForApproveResponse>();

                if (requestForApprove.PrimaryApprovers.Any(x => x.LdapUserId == userId))
                {
                    foreach (var approver in requestForApprove.PrimaryApprovers.Where(approver => approver.LdapUserId == userId))
                    {
                        if (requestForApprove.CheckApproverCanSeeRequest(primaryGroups, approver))
                        {
                            temp.Add(new RequestForApproveResponse
                            {
                                Id = requestForApprove.Id,
                                IsApproved = requestForApprove.CheckApprove(),
                                Name = requestForApprove.RequestTemplate.Name,
                                AuthorName = requestForApprove.AuthorName,
                            });
                        }
                    }
                }

                if (requestForApprove.SecondaryApprovers.Any(x => x.LdapUserId == userId))
                {
                    if (!requestForApprove.PrimaryApprovers.Any(x => x.Approved == true))
                    {
                        var secondaryGroups = requestDataJson.SelectToken("SolutionsGroups")?.ToObject<string[]>();

                        foreach (var approver in requestForApprove.SecondaryApprovers.Where(approver => approver.LdapUserId == userId))
                        {
                            if (requestForApprove.CheckApproverCanSeeRequest(secondaryGroups, approver))
                            {
                                temp.Add(new RequestForApproveResponse
                                {
                                    Id = requestForApprove.Id,
                                    IsApproved = requestForApprove.CheckApprove(),
                                    Name = requestForApprove.RequestTemplate.Name,
                                    AuthorName = requestForApprove.AuthorName,
                                });
                            }
                        }
                    }
                }

                response.RequestForApprove = temp.ToArray();
            }

            if (userSetting.Value.Contains(request.LdapUserId, StringComparison.OrdinalIgnoreCase))
            {
                var allRequests = await _requestRepository.GetAllRequestsAsync(cancellationToken);
                response.AllRequests = allRequests
                    .OrderByDescending(x => x.CreateDate)
                    .Select(x => new RequestForApproveResponse
                    {
                        Id = x.Id,
                        IsApproved = x.CheckApprove(),
                        Name = x.RequestTemplate.Name,
                        AuthorName = x.AuthorName,
                    })
                    .ToArray();
            }

            return response;
        }
    }
}
