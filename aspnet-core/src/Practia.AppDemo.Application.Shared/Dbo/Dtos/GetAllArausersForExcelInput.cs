using Abp.Application.Services.Dto;
using System;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetAllArausersForExcelInput
    {
		public string Filter { get; set; }

		public int? Maxuser_idFilter { get; set; }
		public int? Minuser_idFilter { get; set; }

		public string user_nameFilter { get; set; }

		public string user_real_nameFilter { get; set; }

		public string user_emailFilter { get; set; }


		 public string Araprofileprof_idFilter { get; set; }

		 
    }
}