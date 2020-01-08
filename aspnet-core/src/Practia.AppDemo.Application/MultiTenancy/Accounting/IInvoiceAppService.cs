using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Practia.AppDemo.MultiTenancy.Accounting.Dto;

namespace Practia.AppDemo.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
