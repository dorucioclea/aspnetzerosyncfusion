using Abp.Auditing;
using Practia.AppDemo.Configuration.Dto;

namespace Practia.AppDemo.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}