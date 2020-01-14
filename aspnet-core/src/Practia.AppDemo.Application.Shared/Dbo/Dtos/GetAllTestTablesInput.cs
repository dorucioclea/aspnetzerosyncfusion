using Abp.Application.Services.Dto;
using System;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetAllTestTablesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string CodeFilter { get; set; }



    }
}