﻿
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class CreateOrEditArauserDto : EntityDto<int?>
    {

		public int user_id { get; set; }
		
		
		[Required]
		public string user_name { get; set; }
		
		
		public string user_real_name { get; set; }
		
		
		public string user_email { get; set; }
		
		
		 public int? prof_id { get; set; }
		 
		 
    }
}