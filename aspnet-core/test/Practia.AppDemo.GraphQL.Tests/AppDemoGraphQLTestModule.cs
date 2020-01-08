using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Practia.AppDemo.Configure;
using Practia.AppDemo.Startup;
using Practia.AppDemo.Test.Base;

namespace Practia.AppDemo.GraphQL.Tests
{
    [DependsOn(
        typeof(AppDemoGraphQLModule),
        typeof(AppDemoTestBaseModule))]
    public class AppDemoGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoGraphQLTestModule).GetAssembly());
        }
    }
}