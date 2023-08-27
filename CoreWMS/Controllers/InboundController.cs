using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWMS.Controllers
{
    public class InboundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ControllerName = "Inbound";
            ViewBag.SubNavItems = new List<string>();

            ViewBag.SubNavItems.Add("Orders");
            ViewBag.SubNavItems.Add("Receipts");
        }
    }
}
