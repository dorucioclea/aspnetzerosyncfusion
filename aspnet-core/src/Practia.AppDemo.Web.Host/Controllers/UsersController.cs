using Abp.AspNetCore.Mvc.Authorization;
using Practia.AppDemo.Authorization;
using Practia.AppDemo.Storage;
using Abp.BackgroundJobs;

namespace Practia.AppDemo.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}