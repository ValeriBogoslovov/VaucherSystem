namespace VaucherSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using PagedList;
    using Models.ViewModels.Home;

    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }
        
        [HttpGet]
        public ActionResult Index(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<VaucherViewModel> model = this.service.GetAllVauchers((int)page);

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_AllVauchers", model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("ShowImage/{vaucherId:int}")]
        public FileContentResult ShowImage(int vaucherId)
        {
            var imageData = this.service.GetPicture(vaucherId);

            return File(imageData, "image/jpg");
        }

        [HttpGet]
        [Route("VaucherDetails/{vaucherId:int}")]
        public ActionResult VaucherDetails(int vaucherId)
        {
            var model = this.service.GetVaucherDetails(vaucherId);
            return this.View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        [Route("TopVaucher")]
        public ActionResult TopVaucher()
        {
            var model = this.service.GetTopVaucher();

            return this.View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [Route("CheckboxCategories")]
        public ActionResult CheckboxCategories()
        {
            var model = this.service.AllCategories();

            return this.PartialView("_CheckboxCategories", model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TermsAndConditions")]
        public ActionResult TermsAndConditions()
        {
            return this.View();
        }
        
    }
}