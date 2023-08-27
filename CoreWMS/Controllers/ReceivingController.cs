using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWMS.Controllers
{
    public class ReceivingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ControllerName = "Receiving";
        }
    }
}
