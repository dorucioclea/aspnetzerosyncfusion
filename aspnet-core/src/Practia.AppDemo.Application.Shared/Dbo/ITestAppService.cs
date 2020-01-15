using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo
{
    public interface ITestAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTestForViewDto>> GetAll(GetAllTestInput input);

        Task<GetTestForViewDto> GetTestForView(int id);

		Task<GetTestForEditOutput> GetTestForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTestDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTestToExcel(GetAllTestForExcelInput input);

		
    }
}