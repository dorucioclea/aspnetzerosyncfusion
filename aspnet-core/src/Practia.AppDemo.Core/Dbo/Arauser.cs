using Practia.AppDemo.Dbo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Practia.AppDemo.Dbo
{
	[Table("arausers")]
    public class Arauser : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int user_id { get; set; }
		
		[Required]
		public virtual string user_name { get; set; }
		
		public virtual string user_real_name { get; set; }
		
		public virtual string user_email { get; set; }
		

		public virtual int? prof_id { get; set; }
		
        [ForeignKey("prof_id")]
		public Araprofile prof_Fk { get; set; }
		
    }
}