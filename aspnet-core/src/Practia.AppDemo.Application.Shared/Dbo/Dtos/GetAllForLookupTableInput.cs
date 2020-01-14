using Abp.Application.Services.Dto;

namespace Practia.AppDemo.Dbo.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}