

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
	[AbpAuthorize(AppPermissions.Pages_Test)]
    public class TestAppService : AppDemoAppServiceBase, ITestAppService
    {
		 private readonly IRepository<Test> _testRepository;
		 private readonly ITestExcelExporter _testExcelExporter;
		 

		  public TestAppService(IRepository<Test> testRepository, ITestExcelExporter testExcelExporter ) 
		  {
			_testRepository = testRepository;
			_testExcelExporter = testExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTestForViewDto>> GetAll(GetAllTestInput input)
         {
			
			var filteredTest = _testRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var pagedAndFilteredTest = filteredTest
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var test = from o in pagedAndFilteredTest
                         select new GetTestForViewDto() {
							Test = new TestDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTest.CountAsync();

            return new PagedResultDto<GetTestForViewDto>(
                totalCount,
                await test.ToListAsync()
            );
         }
		 
		 public async Task<GetTestForViewDto> GetTestForView(int id)
         {
            var test = await _testRepository.GetAsync(id);

            var output = new GetTestForViewDto { Test = ObjectMapper.Map<TestDto>(test) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Test_Edit)]
		 public async Task<GetTestForEditOutput> GetTestForEdit(EntityDto input)
         {
            var test = await _testRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTestForEditOutput {Test = ObjectMapper.Map<CreateOrEditTestDto>(test)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Test_Create)]
		 protected virtual async Task Create(CreateOrEditTestDto input)
         {
            var test = ObjectMapper.Map<Test>(input);

			
			if (AbpSession.TenantId != null)
			{
				test.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _testRepository.InsertAsync(test);
         }

		 [AbpAuthorize(AppPermissions.Pages_Test_Edit)]
		 protected virtual async Task Update(CreateOrEditTestDto input)
         {
            var test = await _testRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, test);
         }

		 [AbpAuthorize(AppPermissions.Pages_Test_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _testRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTestToExcel(GetAllTestForExcelInput input)
         {
			
			var filteredTest = _testRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var query = (from o in filteredTest
                         select new GetTestForViewDto() { 
							Test = new TestDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						 });


            var testListDtos = await query.ToListAsync();

            return _testExcelExporter.ExportToFile(testListDtos);
         }


    }
}