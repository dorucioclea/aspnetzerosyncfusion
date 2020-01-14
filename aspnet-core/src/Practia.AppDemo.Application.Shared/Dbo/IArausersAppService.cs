using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo
{
    public interface IArausersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetArauserForViewDto>> GetAll(GetAllArausersInput input);

        Task<GetArauserForViewDto> GetArauserForView(int id);

		Task<GetArauserForEditOutput> GetArauserForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditArauserDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetArausersToExcel(GetAllArausersForExcelInput input);

		
		Task<PagedResultDto<ArauserAraprofileLookupTableDto>> GetAllAraprofileForLookupTable(GetAllForLookupTableInput input);
		
    }
}