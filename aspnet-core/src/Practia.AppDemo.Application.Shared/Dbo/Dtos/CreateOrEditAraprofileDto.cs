
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class CreateOrEditAraprofileDto : EntityDto<int?>
    {

		public int prof_id { get; set; }
		
		
		[Required]
		public string prof_description { get; set; }
		
		

    }
}