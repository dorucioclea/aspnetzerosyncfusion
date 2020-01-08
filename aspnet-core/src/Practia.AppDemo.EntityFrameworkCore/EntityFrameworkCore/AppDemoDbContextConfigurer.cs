using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Practia.AppDemo.EntityFrameworkCore
{
    public static class AppDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AppDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AppDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}