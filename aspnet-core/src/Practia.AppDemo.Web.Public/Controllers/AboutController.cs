using Microsoft.AspNetCore.Mvc;
using Practia.AppDemo.Web.Controllers;

namespace Practia.AppDemo.Web.Public.Controllers
{
    public class AboutController : AppDemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}