using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();

        }

        public ActionResult InternalError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}