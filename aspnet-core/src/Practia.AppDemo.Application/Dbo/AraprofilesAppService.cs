

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Practia.AppDemo.Dbo.Exporting;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Abp.Application.Services.Dto;
using Practia.AppDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Practia.AppDemo.Dbo
{
	[AbpAuthorize(AppPermissions.Pages_Araprofiles)]
    public class AraprofilesAppService : AppDemoAppServiceBase, IAraprofilesAppService
    {
		 private readonly IRepository<Araprofile> _araprofileRepository;
		 private readonly IAraprofilesExcelExporter _araprofilesExcelExporter;
		 

		  public AraprofilesAppService(IRepository<Araprofile> araprofileRepository, IAraprofilesExcelExporter araprofilesExcelExporter ) 
		  {
			_araprofileRepository = araprofileRepository;
			_araprofilesExcelExporter = araprofilesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetAraprofileForViewDto>> GetAll(GetAllAraprofilesInput input)
         {
			
			var filteredAraprofiles = _araprofileRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.prof_description.Contains(input.Filter))
						.WhereIf(input.Minprof_idFilter != null, e => e.prof_id >= input.Minprof_idFilter)
						.WhereIf(input.Maxprof_idFilter != null, e => e.prof_id <= input.Maxprof_idFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.prof_descriptionFilter),  e => e.prof_description == input.prof_descriptionFilter);

			var pagedAndFilteredAraprofiles = filteredAraprofiles
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var araprofiles = from o in pagedAndFilteredAraprofiles
                         select new GetAraprofileForViewDto() {
							Araprofile = new AraprofileDto
							{
                                prof_id = o.prof_id,
                                prof_description = o.prof_description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredAraprofiles.CountAsync();

            return new PagedResultDto<GetAraprofileForViewDto>(
                totalCount,
                await araprofiles.ToListAsync()
            );
         }
		 
		 public async Task<GetAraprofileForViewDto> GetAraprofileForView(int id)
         {
            var araprofile = await _araprofileRepository.GetAsync(id);

            var output = new GetAraprofileForViewDto { Araprofile = ObjectMapper.Map<AraprofileDto>(araprofile) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Araprofiles_Edit)]
		 public async Task<GetAraprofileForEditOutput> GetAraprofileForEdit(EntityDto input)
         {
            var araprofile = await _araprofileRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAraprofileForEditOutput {Araprofile = ObjectMapper.Map<CreateOrEditAraprofileDto>(araprofile)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAraprofileDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Araprofiles_Create)]
		 protected virtual async Task Create(CreateOrEditAraprofileDto input)
         {
            var araprofile = ObjectMapper.Map<Araprofile>(input);

			
			if (AbpSession.TenantId != null)
			{
				araprofile.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _araprofileRepository.InsertAsync(araprofile);
         }

		 [AbpAuthorize(AppPermissions.Pages_Araprofiles_Edit)]
		 protected virtual async Task Update(CreateOrEditAraprofileDto input)
         {
            var araprofile = await _araprofileRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, araprofile);
         }

		 [AbpAuthorize(AppPermissions.Pages_Araprofiles_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _araprofileRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetAraprofilesToExcel(GetAllAraprofilesForExcelInput input)
         {
			
			var filteredAraprofiles = _araprofileRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.prof_description.Contains(input.Filter))
						.WhereIf(input.Minprof_idFilter != null, e => e.prof_id >= input.Minprof_idFilter)
						.WhereIf(input.Maxprof_idFilter != null, e => e.prof_id <= input.Maxprof_idFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.prof_descriptionFilter),  e => e.prof_description == input.prof_descriptionFilter);

			var query = (from o in filteredAraprofiles
                         select new GetAraprofileForViewDto() { 
							Araprofile = new AraprofileDto
							{
                                prof_id = o.prof_id,
                                prof_description = o.prof_description,
                                Id = o.Id
							}
						 });


            var araprofileListDtos = await query.ToListAsync();

            return _araprofilesExcelExporter.ExportToFile(araprofileListDtos);
         }


    }
}