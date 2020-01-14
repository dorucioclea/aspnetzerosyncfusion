using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetArauserForEditOutput
    {
		public CreateOrEditArauserDto Arauser { get; set; }

		public string Araprofileprof_id { get; set;}


    }
}