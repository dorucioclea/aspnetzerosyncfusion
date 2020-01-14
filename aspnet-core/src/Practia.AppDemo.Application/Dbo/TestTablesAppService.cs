

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
	[AbpAuthorize(AppPermissions.Pages_TestTables)]
    public class TestTablesAppService : AppDemoAppServiceBase, ITestTablesAppService
    {
		 private readonly IRepository<TestTable> _testTableRepository;
		 private readonly ITestTablesExcelExporter _testTablesExcelExporter;
		 

		  public TestTablesAppService(IRepository<TestTable> testTableRepository, ITestTablesExcelExporter testTablesExcelExporter ) 
		  {
			_testTableRepository = testTableRepository;
			_testTablesExcelExporter = testTablesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTestTableForViewDto>> GetAll(GetAllTestTablesInput input)
         {
			
			var filteredTestTables = _testTableRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var pagedAndFilteredTestTables = filteredTestTables
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var testTables = from o in pagedAndFilteredTestTables
                         select new GetTestTableForViewDto() {
							TestTable = new TestTableDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTestTables.CountAsync();

            return new PagedResultDto<GetTestTableForViewDto>(
                totalCount,
                await testTables.ToListAsync()
            );
         }
		 
		 public async Task<GetTestTableForViewDto> GetTestTableForView(int id)
         {
            var testTable = await _testTableRepository.GetAsync(id);

            var output = new GetTestTableForViewDto { TestTable = ObjectMapper.Map<TestTableDto>(testTable) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TestTables_Edit)]
		 public async Task<GetTestTableForEditOutput> GetTestTableForEdit(EntityDto input)
         {
            var testTable = await _testTableRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTestTableForEditOutput {TestTable = ObjectMapper.Map<CreateOrEditTestTableDto>(testTable)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTestTableDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_TestTables_Create)]
		 protected virtual async Task Create(CreateOrEditTestTableDto input)
         {
			try
			{
				var testTable = ObjectMapper.Map<TestTable>(input);


				if (AbpSession.TenantId != null)
				{
					testTable.TenantId = (int?)AbpSession.TenantId;
				}


				await _testTableRepository.InsertAsync(testTable);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_TestTables_Edit)]
		 protected virtual async Task Update(CreateOrEditTestTableDto input)
         {
            var testTable = await _testTableRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, testTable);
         }

		 [AbpAuthorize(AppPermissions.Pages_TestTables_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _testTableRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTestTablesToExcel(GetAllTestTablesForExcelInput input)
         {
			
			var filteredTestTables = _testTableRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var query = (from o in filteredTestTables
                         select new GetTestTableForViewDto() { 
							TestTable = new TestTableDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						 });


            var testTableListDtos = await query.ToListAsync();

            return _testTablesExcelExporter.ExportToFile(testTableListDtos);
         }


    }
}