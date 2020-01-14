using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Practia.AppDemo.Dbo
{
	[Table("araprofiles")]
    public class Araprofile : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int prof_id { get; set; }
		
		[Required]
		public virtual string prof_description { get; set; }
		

    }
}