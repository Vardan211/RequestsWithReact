using MediatR;
using Requests.Application.Abstractions;
using Requests.Domain.Constants;

namespace Requests.Application.Settings.V1.GetTabs
{
    public class GetTabsQueryHandler : IRequestHandler<GetTabsQuery, string[]>
    {
        private readonly ISettingRepository _settingRepository;

        public GetTabsQueryHandler(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
        }

        public async Task<string[]> Handle(GetTabsQuery request, CancellationToken cancellationToken)
        {
            var setting = await _settingRepository.GetSettingByKeyAsync(SettingsNames.RequestViewer, cancellationToken);

            return setting.Value.Contains(request.LdapUserId, StringComparison.OrdinalIgnoreCase)
            ? new string[] { TabsNames.CreateRequest, TabsNames.MyRequests, TabsNames.RequestsForApprove, TabsNames.AllRequests }
            : new string[] { TabsNames.CreateRequest, TabsNames.MyRequests, TabsNames.RequestsForApprove };
        }
    }
}
