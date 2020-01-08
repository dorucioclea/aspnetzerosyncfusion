using Abp.AspNetCore.Mvc.Authorization;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(ITempFileCacheManager tempFileCacheManager) :
            base(tempFileCacheManager)
        {
        }
    }
}