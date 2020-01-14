using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetTestTableForEditOutput
    {
		public CreateOrEditTestTableDto TestTable { get; set; }


    }
}