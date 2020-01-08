using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Configuration.Tenants.Dto;

namespace Practia.AppDemo.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
