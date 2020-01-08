using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Configuration.Host.Dto;

namespace Practia.AppDemo.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
