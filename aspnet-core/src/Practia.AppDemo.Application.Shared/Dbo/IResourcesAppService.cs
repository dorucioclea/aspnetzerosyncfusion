using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo
{
    public interface IResourcesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetResourceForViewDto>> GetAll(GetAllResourcesInput input);

        Task<GetResourceForViewDto> GetResourceForView(int id);

		Task<GetResourceForEditOutput> GetResourceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditResourceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetResourcesToExcel(GetAllResourcesForExcelInput input);

		
    }
}