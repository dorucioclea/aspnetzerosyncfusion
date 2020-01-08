using Practia.AppDemo.EntityFrameworkCore;

namespace Practia.AppDemo.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly AppDemoDbContext _context;

        public InitialHostDbBuilder(AppDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
