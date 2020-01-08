using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo
{
    public class AppDemoClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoClientModule).GetAssembly());
        }
    }
}
