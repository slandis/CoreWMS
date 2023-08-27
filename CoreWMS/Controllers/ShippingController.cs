using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWMS.Controllers
{
    public class ShippingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ControllerName = "Shipping";
        }
    }
}
