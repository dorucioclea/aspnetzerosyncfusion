using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    [DependsOn(typeof(AppDemoXamarinSharedModule))]
    public class AppDemoXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoXamarinAndroidModule).GetAssembly());
        }
    }
}