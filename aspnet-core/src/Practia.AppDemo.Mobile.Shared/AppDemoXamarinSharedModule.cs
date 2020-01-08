using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    [DependsOn(typeof(AppDemoClientModule), typeof(AbpAutoMapperModule))]
    public class AppDemoXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoXamarinSharedModule).GetAssembly());
        }
    }
}