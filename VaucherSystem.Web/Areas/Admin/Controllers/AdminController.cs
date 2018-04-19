namespace VaucherSystem.Web.Areas.Admin.Controllers
{
    using Extensions;
    using Models.BindingModels.Admin;
    using Models.ViewModels.Admin;
    using PagedList;
    using Services.Contracts;
    using System.Web.Mvc;

    [RouteArea("Admin", AreaPrefix ="")]
    [RoutePrefix("Admin")]
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [Route("AllPendingMerchants/{page:int?}")]
        [HttpGet]
        public ActionResult AllPendingMerchants(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }
            IPagedList<MerchantViewModel> model = this.service.GetPendingMerchantsPerPage((int)page);

            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView("_PendingMerchants", model);
            }

            return this.View(model);
        }

        [Route("AllMerchants/{page:int?}")]
        [HttpGet]
        public ActionResult AllMerchants(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<MerchantViewModel> model = this.service.GetAllMerchantsPerPage((int)page);
            if (Request != null && Request.IsAjaxRequest())
            {
                return this.PartialView("_AllMerchants", model);
            }

            return this.View(model);
        }

        [Route("AllCustomers/{page:int?}")]
        [HttpGet]
        public ActionResult AllCustomers(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<CustomerViewModel> model = this.service.GetAllCustomersPerPage((int)page);
            if (Request != null && Request.IsAjaxRequest())
            {
                return this.PartialView("_AllCustomers", model);
            }

            return this.View(model);
        }

        [Route("AllCategories/{page:int?}")]
        [HttpGet]
        public ActionResult AllCategories(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<CategoryViewModel> model = this.service.GetAllCategoriesPerPage((int)page);
            
            if (Request != null && Request.IsAjaxRequest())
            {
                return this.PartialView("_AllCategories", model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("AddCategory")]
        public ActionResult AddCategory()
        {
            return this.PartialView("_AddCategory");
        }

        [HttpPost]
        [Route("AddCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(AddCategoryBindingModel bm)
        {
            if(!ModelState.IsValid || !this.service.AddCategory(bm))
            {
                this.AddNotification("Failed to create category.", NotificationType.ERROR);
                return this.RedirectToAction("AllCategories");
            }
            this.AddNotification("Category created.", NotificationType.SUCCESS);
            return this.RedirectToAction("AllCategories");
        }

        [HttpPost]
        [Route("RemoveCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCategory(RemoveCategoryBindingModel bm)
        {
            if (!ModelState.IsValid || !this.service.RemoveCategory(bm))
            {
                this.AddNotification("Failed to remove category.", NotificationType.ERROR);
                return this.RedirectToAction("AllCategories");
            }
            this.AddNotification("Category removed", NotificationType.SUCCESS);
            return this.RedirectToAction("AllCategories");
        }

        [HttpGet]
        [Route("MerchantDetails/{id:int}")]
        public ActionResult MerchantDetails(int id)
        {
            var model = this.service.GetMerchant(id);
            return this.PartialView("_MerchantDetails", model);
        }

        [HttpPost]
        [Route("DenyAccess")]
        [ValidateAntiForgeryToken]
        public ActionResult DenyAccess(MerchantDenyAccessBindingModel bm)
        {
            if (!ModelState.IsValid || !this.service.DenyMerchantAccess(bm))
            {
                this.AddNotification("Failed to deny merchant access!", NotificationType.ERROR);
                return this.RedirectToAction("AllMerchants");
            }
            this.AddNotification("Denied merchant access!", NotificationType.SUCCESS);
            return this.RedirectToAction("AllMerchants");
        }

        [HttpPost]
        [Route("AllowAccess")]
        [ValidateAntiForgeryToken]
        public ActionResult AllowAccess(MerchantAllowAccessBindingModel bm)
        {
            if (!ModelState.IsValid || !this.service.AllowMerchantAccess(bm))
            {
                this.AddNotification("Failed to allow access!", NotificationType.ERROR);
                return this.RedirectToAction("AllPendingMerchants");
            }
            this.AddNotification("Merchant access allowed!", NotificationType.SUCCESS);
            return this.RedirectToAction("AllPendingMerchants");
        }

        [Route("BannedCustomers/{page:int?}")]
        [HttpGet]
        public ActionResult BannedCustomers(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            IPagedList<BannedCustomerViewModel> model = this.service.GetAllBannedCustomersPerPage((int)page);
            if (Request != null && Request.IsAjaxRequest())
            {
                return this.PartialView("_BannedCustomers", model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("CustomerDetails/{id:int}")]
        public ActionResult CustomerDetails(int id)
        {
            var model = this.service.GetCustomer(id);
            return this.PartialView("_CustomerDetails", model);
        }

        [HttpPost]
        [Route("BanCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult BanCustomer(BanUnBanCustomerBindingModel bm)
        {
            if (!ModelState.IsValid || !this.service.IsCustomerBanned(bm))
            {
                this.AddNotification("Failed to ban customer!", NotificationType.ERROR);
                return this.RedirectToAction("AllCustomers");
            }
            this.AddNotification("Customer banned!", NotificationType.SUCCESS);
            return this.RedirectToAction("AllCustomers");
        }

        [HttpPost]
        [Route("UnBanCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult UnBanCustomer(BanUnBanCustomerBindingModel bm)
        {
            if (!ModelState.IsValid || !this.service.UnBanCustomer(bm))
            {
                this.AddNotification("Failed unban customer!", NotificationType.ERROR);
                return this.RedirectToAction("BannedCustomers");
            }
            this.AddNotification("Customer unbanned!", NotificationType.SUCCESS);
            return this.RedirectToAction("BannedCustomers");
        }
        
        [HttpGet]
        [Route("PendingVauchers/{page:int?}")]
        public ActionResult PendingVauchers(int? page)
        {
            if (page <= 0 || page == null)
            {
                page = 1;
            }

            var model = this.service.GetPendingVauchers((int)page);

            if (Request != null && Request.IsAjaxRequest())
            {
                return this.PartialView("_PendingVauchers");
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("PendingVaucherImage/{Id:int}")]
        public FileContentResult PendingVaucherImage(int Id)
        {
            var imageData = this.service.GetVaucherImage(Id);

            return File(imageData, "image/jpg");
        }

        [HttpPost]
        [Route("ActivateVaucher")]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateVaucher(ActivateVaucherBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                this.AddNotification($"Failed to activate vaucher with id:{bm.Id}!", NotificationType.ERROR);
                return this.RedirectToAction("PendingVauchers");
            }
            this.service.ActivateVaucher(bm);
            this.AddNotification("Vaucher activated!", NotificationType.SUCCESS);
            return this.RedirectToAction("PendingVauchers");
        }
    }
}