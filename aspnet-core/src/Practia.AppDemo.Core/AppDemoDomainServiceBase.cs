using Abp.Domain.Services;

namespace Practia.AppDemo
{
    public abstract class AppDemoDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected AppDemoDomainServiceBase()
        {
            LocalizationSourceName = AppDemoConsts.LocalizationSourceName;
        }
    }
}
