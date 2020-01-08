using Abp.AspNetCore.Mvc.ViewComponents;

namespace Practia.AppDemo.Web.Public.Views
{
    public abstract class AppDemoViewComponent : AbpViewComponent
    {
        protected AppDemoViewComponent()
        {
            LocalizationSourceName = AppDemoConsts.LocalizationSourceName;
        }
    }
}