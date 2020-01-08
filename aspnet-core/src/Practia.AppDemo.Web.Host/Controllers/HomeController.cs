using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Practia.AppDemo.Web.Controllers
{
    public class HomeController : AppDemoControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ui");
        }
    }
}
