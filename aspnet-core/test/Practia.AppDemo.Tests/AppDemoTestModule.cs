using Abp.Modules;
using Practia.AppDemo.Test.Base;

namespace Practia.AppDemo.Tests
{
    [DependsOn(typeof(AppDemoTestBaseModule))]
    public class AppDemoTestModule : AbpModule
    {
       
    }
}
