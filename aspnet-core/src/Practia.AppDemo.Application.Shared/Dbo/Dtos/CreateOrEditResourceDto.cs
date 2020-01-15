
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class CreateOrEditResourceDto : EntityDto<int?>
    {

		[Required]
		public string Name { get; set; }
		
		
		[Required]
		public string Code { get; set; }
		
		

    }
}