
using System;
using Abp.Application.Services.Dto;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class ArauserDto : EntityDto
    {
		public int user_id { get; set; }

		public string user_name { get; set; }

		public string user_real_name { get; set; }

		public string user_email { get; set; }


		 public int? prof_id { get; set; }

		 
    }
}