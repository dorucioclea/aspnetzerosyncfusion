using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Practia.AppDemo.Configuration;
using Practia.AppDemo.Web;

namespace Practia.AppDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AppDemoDbContextFactory : IDesignTimeDbContextFactory<AppDemoDbContext>
    {
        public AppDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            AppDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppDemoConsts.ConnectionStringName));

            return new AppDemoDbContext(builder.Options);
        }
    }
}