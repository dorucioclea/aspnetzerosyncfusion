using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    public class AppDemoCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoCoreSharedModule).GetAssembly());
        }
    }
}