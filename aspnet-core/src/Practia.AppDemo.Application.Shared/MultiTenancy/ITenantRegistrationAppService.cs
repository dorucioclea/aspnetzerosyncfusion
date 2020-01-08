using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Editions.Dto;
using Practia.AppDemo.MultiTenancy.Dto;

namespace Practia.AppDemo.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}