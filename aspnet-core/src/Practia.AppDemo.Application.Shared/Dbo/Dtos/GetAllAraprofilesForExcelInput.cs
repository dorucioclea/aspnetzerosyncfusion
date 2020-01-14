using Abp.Application.Services.Dto;
using System;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetAllAraprofilesForExcelInput
    {
		public string Filter { get; set; }

		public int? Maxprof_idFilter { get; set; }
		public int? Minprof_idFilter { get; set; }

		public string prof_descriptionFilter { get; set; }



    }
}