

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
	[AbpAuthorize(AppPermissions.Pages_Resources)]
    public class ResourcesAppService : AppDemoAppServiceBase, IResourcesAppService
    {
		 private readonly IRepository<Resource> _resourceRepository;
		 private readonly IResourcesExcelExporter _resourcesExcelExporter;
		 

		  public ResourcesAppService(IRepository<Resource> resourceRepository, IResourcesExcelExporter resourcesExcelExporter ) 
		  {
			_resourceRepository = resourceRepository;
			_resourcesExcelExporter = resourcesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetResourceForViewDto>> GetAll(GetAllResourcesInput input)
         {

			var asd = await _resourceRepository.GetAll().ToListAsync();

			var filteredResources = _resourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var pagedAndFilteredResources = filteredResources
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var resources = from o in pagedAndFilteredResources
                         select new GetResourceForViewDto() {
							Resource = new ResourceDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						};

            var totalCount = await filteredResources.CountAsync();

            return new PagedResultDto<GetResourceForViewDto>(
                totalCount,
                await resources.ToListAsync()
            );
         }
		 
		 public async Task<GetResourceForViewDto> GetResourceForView(int id)
         {
            var resource = await _resourceRepository.GetAsync(id);

            var output = new GetResourceForViewDto { Resource = ObjectMapper.Map<ResourceDto>(resource) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Resources_Edit)]
		 public async Task<GetResourceForEditOutput> GetResourceForEdit(EntityDto input)
         {
            var resource = await _resourceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetResourceForEditOutput {Resource = ObjectMapper.Map<CreateOrEditResourceDto>(resource)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditResourceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Resources_Create)]
		 protected virtual async Task Create(CreateOrEditResourceDto input)
         {
            var resource = ObjectMapper.Map<Resource>(input);

			
			if (AbpSession.TenantId != null)
			{
				resource.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _resourceRepository.InsertAsync(resource);
         }

		 [AbpAuthorize(AppPermissions.Pages_Resources_Edit)]
		 protected virtual async Task Update(CreateOrEditResourceDto input)
         {
            var resource = await _resourceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, resource);
         }

		 [AbpAuthorize(AppPermissions.Pages_Resources_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _resourceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetResourcesToExcel(GetAllResourcesForExcelInput input)
         {
			
			var filteredResources = _resourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var query = (from o in filteredResources
                         select new GetResourceForViewDto() { 
							Resource = new ResourceDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						 });


            var resourceListDtos = await query.ToListAsync();

            return _resourcesExcelExporter.ExportToFile(resourceListDtos);
         }


    }
}