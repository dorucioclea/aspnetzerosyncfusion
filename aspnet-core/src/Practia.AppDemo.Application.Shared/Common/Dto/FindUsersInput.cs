using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}