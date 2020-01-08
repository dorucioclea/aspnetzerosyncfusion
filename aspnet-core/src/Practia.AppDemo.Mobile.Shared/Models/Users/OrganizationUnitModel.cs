using Abp.AutoMapper;
using Practia.AppDemo.Organizations.Dto;

namespace Practia.AppDemo.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}