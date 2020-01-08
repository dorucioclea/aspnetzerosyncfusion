using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    [DependsOn(typeof(AppDemoCoreSharedModule))]
    public class AppDemoApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoApplicationSharedModule).GetAssembly());
        }
    }
}