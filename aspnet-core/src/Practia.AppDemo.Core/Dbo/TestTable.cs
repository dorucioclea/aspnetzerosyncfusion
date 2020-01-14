using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Practia.AppDemo.Dbo
{
	[Table("TestTable")]
    public class TestTable : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual string Name { get; set; }
		
		[Required]
		public virtual string Code { get; set; }
		

    }
}