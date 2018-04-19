namespace VaucherSystem.Web.Areas.Merchant.Controllers
{
    using Extensions;
    using Models.BindingModels.Merchant;
    using Models.ViewModels.Home;
    using Models.ViewModels.Merchant;
    using PagedList;
    using Services.Contracts;
    using System.Web;
    using System.Web.Mvc;

    [RouteArea("Merchant")]
    [RoutePrefix("Merchant")]
    [Authorize(Roles = "Merchant")]

    public class MerchantController : Controller
    {
        private IMerchantService service;

        public MerchantController(IMerchantService service)
        {
            this.service = service;
        }

        [Route("MyActiveVauchers/{page:int?}")]
        [HttpGet]
        public ActionResult MyActiveVauchers(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<MerchantVaucherViewModel> model = this.service.GetMyActiveVauchersPerPage(
                                        (int)page, this.User.Identity.Name);

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_MyActiveVauchers", model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("MerchantVaucherImage/{vaucherId:int}")]
        public FileContentResult MerchantVaucherImage(int vaucherId)
        {
            var imageData = this.service.GetPicture(vaucherId);

            return File(imageData, "image/jpg");
        }

        [HttpGet]//TO DO
        [Route("ShowImage/{vaucherId:int}")]
        public FileContentResult ShowImages(int vaucherId)
        {
            var imageData = this.service.GetPictures(vaucherId);

            return null;
        }

        [Route("MyPassedVauchers/{page:int?}")]
        [HttpGet]
        public ActionResult MyPassedVauchers(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<MerchantVaucherViewModel> model = this.service.GetMyPassedVauchersPerPage((int)page, this.User.Identity.Name);

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_MyPassedVauchers", model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("CreateVaucher")]
        public ActionResult CreateVaucher()
        {
            return this.View();
        }

        [HttpPost]
        [Route("CreateVaucher")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVaucher(CreateVaucherBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                this.AddNotification("Failed to create vaucher!", NotificationType.ERROR);
                return this.RedirectToAction("CreateVaucher");
            }
            this.service.CreateVaucher(bm, this.User.Identity.Name);
            this.AddNotification("Vaucher created", NotificationType.SUCCESS);
            return this.RedirectToAction("CreateVaucher");
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public ActionResult GetAllCategories()
        {
            var model = this.service.GetCategories();
            return this.PartialView("_AllCategories", model);
        }

        [HttpGet]
        [Route("MerchantVaucherDetails/{vaucherId:int}")]
        public ActionResult MerchantVaucherDetails(int vaucherId)
        {
            var model = this.service.GetVaucherDetails(vaucherId);
            return this.View(model);
        }

        [HttpPost]
        [Route("RemoveVaucher")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveVaucher(RemoveVaucherBindingModel bm)
        {
            if (!ModelState.IsValid || bm.Id <= 0)
            {
                this.AddNotification("Failed to remove vaucher!", NotificationType.ERROR);
                return this.RedirectToAction("MyActiveVauchers");
            }
            this.service.RemoveVaucher(bm);
            this.AddNotification("Vaucher removed!", NotificationType.SUCCESS);
            return this.RedirectToAction("MyActiveVauchers");
        }

        [HttpGet]
        [Route("BoughtVauchers")]
        public ActionResult BoughtVauchers()
        {
            var model = this.service.GetMyBoughtVauchers(this.User.Identity.Name);

            return this.View(model);
        }

        [HttpGet]
        [Route("BoughtVaucherDetails/{id:int}")]
        public ActionResult BoughtVaucherDetails(int id)
        {
            if (id <= 0)
            {
                this.AddNotification($"Failed to retrieve vaucher with id {id}!",NotificationType.ERROR);
                return this.RedirectToAction("BoughtVauchers");
            }
            var model = this.service.GetBoughtVaucherDetails(id);

            return this.View(model);
        }
    }
}