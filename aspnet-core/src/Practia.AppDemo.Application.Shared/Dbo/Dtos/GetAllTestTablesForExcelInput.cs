using Abp.Application.Services.Dto;
using System;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetAllTestTablesForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string CodeFilter { get; set; }



    }
}