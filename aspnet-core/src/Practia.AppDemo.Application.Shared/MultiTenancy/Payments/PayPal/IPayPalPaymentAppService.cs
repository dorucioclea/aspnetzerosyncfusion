using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.MultiTenancy.Payments.PayPal.Dto;

namespace Practia.AppDemo.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
