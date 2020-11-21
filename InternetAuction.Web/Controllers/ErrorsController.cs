using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    /// <summary>
    /// Represents error controller class
    /// </summary>
    public class ErrorsController : Controller
    {
        /// <summary>
        /// Returns not found view
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Returns internal error view
        /// </summary>
        /// <returns></returns>
        public ActionResult InternalError()
        {
            Response.StatusCode = 500;
            return View();
        }

        /// <summary>
        /// Returns forbidden view
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}