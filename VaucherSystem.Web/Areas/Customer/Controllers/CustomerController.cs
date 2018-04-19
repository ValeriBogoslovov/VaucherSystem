namespace VaucherSystem.Web.Areas.Customer.Controllers
{
    using Extensions;
    using Models.BindingModels.Customer;
    using Services.Contracts;
    using System.Web.Mvc;

    [RouteArea("Customer")]
    [RoutePrefix("Customer")]
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("MyVauchers")]
        public ActionResult MyVauchers()
        {
            var model = this.service.GetMyVauchers(this.User.Identity.Name);
            return this.View(model);
        }

        [HttpGet]
        [Route("MyVaucherDetails/{id:int}")]
        public ActionResult MyVaucherDetails(int id)
        {
            var model = this.service.GetMyVaucherDetails(id, this.User.Identity.Name);
            return this.View(model);
        }

        [HttpPost]
        [Route("BuyVaucher")]
        [ValidateAntiForgeryToken]
        public ActionResult BuyVaucher(BuyVaucherBindingModel bm)
        {
            if (!this.ModelState.IsValid || !this.service.BuyVaucher(bm, this.User.Identity.Name))
            {
                this.AddNotification("You must agree with our Terms and Conditions to buy the vaucher!", NotificationType.WARNING);
                return this.RedirectToAction($"VaucherDetails/{bm.Id}", "Home", new { area = "" });
            }
            this.AddNotification("Vaucher bought!", NotificationType.SUCCESS);
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [Route("MyProfile")]
        public ActionResult MyProfile()
        {
            var model = this.service.MyProfile(this.User.Identity.Name);

            return this.View(model);
        }
    }
}