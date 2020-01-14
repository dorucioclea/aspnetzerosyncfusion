using Practia.AppDemo.Dbo;


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
	[AbpAuthorize(AppPermissions.Pages_Arausers)]
    public class ArausersAppService : AppDemoAppServiceBase, IArausersAppService
    {
		 private readonly IRepository<Arauser> _arauserRepository;
		 private readonly IArausersExcelExporter _arausersExcelExporter;
		 private readonly IRepository<Araprofile,int> _lookup_araprofileRepository;
		 

		  public ArausersAppService(IRepository<Arauser> arauserRepository, IArausersExcelExporter arausersExcelExporter , IRepository<Araprofile, int> lookup_araprofileRepository) 
		  {
			_arauserRepository = arauserRepository;
			_arausersExcelExporter = arausersExcelExporter;
			_lookup_araprofileRepository = lookup_araprofileRepository;
		
		  }

		 public async Task<PagedResultDto<GetArauserForViewDto>> GetAll(GetAllArausersInput input)
         {
			
			var filteredArausers = _arauserRepository.GetAll()
						.Include( e => e.prof_Fk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.user_name.Contains(input.Filter) || e.user_real_name.Contains(input.Filter) || e.user_email.Contains(input.Filter))
						.WhereIf(input.Minuser_idFilter != null, e => e.user_id >= input.Minuser_idFilter)
						.WhereIf(input.Maxuser_idFilter != null, e => e.user_id <= input.Maxuser_idFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_nameFilter),  e => e.user_name == input.user_nameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_real_nameFilter),  e => e.user_real_name == input.user_real_nameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_emailFilter),  e => e.user_email == input.user_emailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Araprofileprof_idFilter), e => e.prof_Fk != null && e.prof_Fk.prof_id.ToString() == input.Araprofileprof_idFilter);

			var pagedAndFilteredArausers = filteredArausers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var arausers = from o in pagedAndFilteredArausers
                         join o1 in _lookup_araprofileRepository.GetAll() on o.prof_id equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetArauserForViewDto() {
							Arauser = new ArauserDto
							{
                                user_id = o.user_id,
                                user_name = o.user_name,
                                user_real_name = o.user_real_name,
                                user_email = o.user_email,
                                Id = o.Id
							},
                         	Araprofileprof_id = s1 == null ? "" : s1.prof_id.ToString()
						};

            var totalCount = await filteredArausers.CountAsync();

            return new PagedResultDto<GetArauserForViewDto>(
                totalCount,
                await arausers.ToListAsync()
            );
         }
		 
		 public async Task<GetArauserForViewDto> GetArauserForView(int id)
         {
            var arauser = await _arauserRepository.GetAsync(id);

            var output = new GetArauserForViewDto { Arauser = ObjectMapper.Map<ArauserDto>(arauser) };

		    if (output.Arauser.prof_id != null)
            {
                var _lookupAraprofile = await _lookup_araprofileRepository.FirstOrDefaultAsync((int)output.Arauser.prof_id);
                output.Araprofileprof_id = _lookupAraprofile.prof_id.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Arausers_Edit)]
		 public async Task<GetArauserForEditOutput> GetArauserForEdit(EntityDto input)
         {
            var arauser = await _arauserRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetArauserForEditOutput {Arauser = ObjectMapper.Map<CreateOrEditArauserDto>(arauser)};

		    if (output.Arauser.prof_id != null)
            {
                var _lookupAraprofile = await _lookup_araprofileRepository.FirstOrDefaultAsync((int)output.Arauser.prof_id);
                output.Araprofileprof_id = _lookupAraprofile.prof_id.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditArauserDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Arausers_Create)]
		 protected virtual async Task Create(CreateOrEditArauserDto input)
         {
            var arauser = ObjectMapper.Map<Arauser>(input);

			
			if (AbpSession.TenantId != null)
			{
				arauser.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _arauserRepository.InsertAsync(arauser);
         }

		 [AbpAuthorize(AppPermissions.Pages_Arausers_Edit)]
		 protected virtual async Task Update(CreateOrEditArauserDto input)
         {
            var arauser = await _arauserRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, arauser);
         }

		 [AbpAuthorize(AppPermissions.Pages_Arausers_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _arauserRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetArausersToExcel(GetAllArausersForExcelInput input)
         {
			
			var filteredArausers = _arauserRepository.GetAll()
						.Include( e => e.prof_Fk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.user_name.Contains(input.Filter) || e.user_real_name.Contains(input.Filter) || e.user_email.Contains(input.Filter))
						.WhereIf(input.Minuser_idFilter != null, e => e.user_id >= input.Minuser_idFilter)
						.WhereIf(input.Maxuser_idFilter != null, e => e.user_id <= input.Maxuser_idFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_nameFilter),  e => e.user_name == input.user_nameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_real_nameFilter),  e => e.user_real_name == input.user_real_nameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.user_emailFilter),  e => e.user_email == input.user_emailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Araprofileprof_idFilter), e => e.prof_Fk != null && e.prof_Fk.prof_id.ToString() == input.Araprofileprof_idFilter);

			var query = (from o in filteredArausers
                         join o1 in _lookup_araprofileRepository.GetAll() on o.prof_id equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetArauserForViewDto() { 
							Arauser = new ArauserDto
							{
                                user_id = o.user_id,
                                user_name = o.user_name,
                                user_real_name = o.user_real_name,
                                user_email = o.user_email,
                                Id = o.Id
							},
                         	Araprofileprof_id = s1 == null ? "" : s1.prof_id.ToString()
						 });


            var arauserListDtos = await query.ToListAsync();

            return _arausersExcelExporter.ExportToFile(arauserListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Arausers)]
         public async Task<PagedResultDto<ArauserAraprofileLookupTableDto>> GetAllAraprofileForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_araprofileRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.prof_id.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var araprofileList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ArauserAraprofileLookupTableDto>();
			foreach(var araprofile in araprofileList){
				lookupTableDtoList.Add(new ArauserAraprofileLookupTableDto
				{
					Id = araprofile.Id,
					DisplayName = araprofile.prof_id.ToString()
				});
			}

            return new PagedResultDto<ArauserAraprofileLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}