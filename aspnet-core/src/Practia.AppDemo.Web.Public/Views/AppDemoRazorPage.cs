using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Practia.AppDemo.Web.Public.Views
{
    public abstract class AppDemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected AppDemoRazorPage()
        {
            LocalizationSourceName = AppDemoConsts.LocalizationSourceName;
        }
    }
}
