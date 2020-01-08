using Abp.AspNetCore.Mvc.Views;

namespace Practia.AppDemo.Web.Views
{
    public abstract class AppDemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected AppDemoRazorPage()
        {
            LocalizationSourceName = AppDemoConsts.LocalizationSourceName;
        }
    }
}
