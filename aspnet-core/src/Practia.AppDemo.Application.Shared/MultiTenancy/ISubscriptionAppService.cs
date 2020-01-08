using System.Threading.Tasks;
using Abp.Application.Services;

namespace Practia.AppDemo.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
