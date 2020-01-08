using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Configuration.Dto;

namespace Practia.AppDemo.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<List<ThemeSettingsDto>> GetUiManagementSettings();

        Task UpdateUiManagementSettings(ThemeSettingsDto settings);

        Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

        Task UseSystemDefaultSettings();
    }
}
