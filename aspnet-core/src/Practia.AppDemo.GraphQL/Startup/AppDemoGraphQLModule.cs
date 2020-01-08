using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Practia.AppDemo.Startup
{
    [DependsOn(typeof(AppDemoCoreModule))]
    public class AppDemoGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppDemoGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}