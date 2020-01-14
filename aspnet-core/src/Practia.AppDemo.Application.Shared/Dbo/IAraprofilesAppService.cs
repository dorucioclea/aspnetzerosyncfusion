using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo
{
    public interface IAraprofilesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAraprofileForViewDto>> GetAll(GetAllAraprofilesInput input);

        Task<GetAraprofileForViewDto> GetAraprofileForView(int id);

		Task<GetAraprofileForEditOutput> GetAraprofileForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditAraprofileDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetAraprofilesToExcel(GetAllAraprofilesForExcelInput input);

		
    }
}