using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo
{
    public interface ITestTablesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTestTableForViewDto>> GetAll(GetAllTestTablesInput input);

        Task<GetTestTableForViewDto> GetTestTableForView(int id);

		Task<GetTestTableForEditOutput> GetTestTableForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTestTableDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTestTablesToExcel(GetAllTestTablesForExcelInput input);

		
    }
}