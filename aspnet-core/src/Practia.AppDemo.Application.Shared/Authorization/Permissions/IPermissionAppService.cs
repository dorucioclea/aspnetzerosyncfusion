using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Authorization.Permissions.Dto;

namespace Practia.AppDemo.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
