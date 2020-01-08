using System;
using Practia.AppDemo.Core;
using Practia.AppDemo.Core.Dependency;
using Practia.AppDemo.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practia.AppDemo.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}