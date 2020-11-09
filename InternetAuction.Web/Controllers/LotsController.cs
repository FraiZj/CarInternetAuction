using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    public class LotsController : Controller
    {
        private readonly ILotService lotService;

        public LotsController(ILotService lotService)
        {
            this.lotService = lotService;
        }

        public ActionResult AllActiveLots()
        {
            var lots = lotService.GetAllActiveLots();
            return View(lots);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var lotModel = new LotModel();
            return View(lotModel);
        }

        [HttpPost]
        public ActionResult Create(LotModel model)
        {
            return View("Details", model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}