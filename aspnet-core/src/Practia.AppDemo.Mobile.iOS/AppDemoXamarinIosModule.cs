using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    [DependsOn(typeof(AppDemoXamarinSharedModule))]
    public class AppDemoXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoXamarinIosModule).GetAssembly());
        }
    }
}