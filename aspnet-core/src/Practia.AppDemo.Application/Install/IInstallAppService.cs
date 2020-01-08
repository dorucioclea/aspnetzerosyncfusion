using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Install.Dto;

namespace Practia.AppDemo.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}