namespace VaucherSystem.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Areas.Admin.Controllers;
    using Moq;
    using Models.ViewModels.Admin;
    using System.Collections.Generic;
    using PagedList;
    using TestStack.FluentMVCTesting;
    using Models.BindingModels.Admin;
    using VaucherSystem.Services.Contracts;

    [TestClass]
    public class AdminControllerTests
    {
        private Mock<IAdminService> service;

        [TestInitialize]
        public void Initialize()
        {
            this.service = new Mock<IAdminService>();
        }

        [TestMethod]
        public void AllPendingMerchant_ShouldReturnPagedListOfPendingMerchants()
        {
            this.service.Setup(s => s.GetPendingMerchantsPerPage(It.IsAny<int>()))
                .Returns(new PagedList<MerchantViewModel>(new List<MerchantViewModel>()
                {
                    new MerchantViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllPendingMerchants(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<MerchantViewModel>>();
                
        }

        [TestMethod]
        public void AllMerchants_ShouldReturnPagedListOfNonPendingMerchants()
        {
            this.service.Setup(s => s.GetAllMerchantsPerPage(It.IsAny<int>()))
                .Returns(new PagedList<MerchantViewModel>(new List<MerchantViewModel>()
                {
                    new MerchantViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllMerchants(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<MerchantViewModel>>();

        }

        [TestMethod]
        public void AllCustomers_ShouldReturnPagedListOfCustomers()
        {
            this.service.Setup(s => s.GetAllCustomersPerPage(It.IsAny<int>()))
                .Returns(new PagedList<CustomerViewModel>(new List<CustomerViewModel>()
                {
                    new CustomerViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllCustomers(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<CustomerViewModel>>();

        }

        [TestMethod]
        public void AllCategories_ShouldReturnPagedListOfAllCategories()
        {
            this.service.Setup(s => s.GetAllCategoriesPerPage(It.IsAny<int>()))
                .Returns(new PagedList<CategoryViewModel>(new List<CategoryViewModel>()
                {
                    new CategoryViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllCategories(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<CategoryViewModel>>();

        }

        [TestMethod]
        public void AddCategory_ShouldReturnPartialView()
        {
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AddCategory())
                .ShouldRenderPartialView("_AddCategory");
        }

        [TestMethod]
        public void AddCategory_ShouldRedirectTo_AllCategories_IfServiceRetursTrue()
        {
            this.service.Setup(s => s.AddCategory(new AddCategoryBindingModel() { Name = "Somet" }))
                .Returns(true);
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AddCategory(new AddCategoryBindingModel() { Name = "Somet" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCategories"));
        }

        [TestMethod]
        public void AddCategory_ShouldRedirectTo_AllCategories_IfServiceRetursFalse()
        {
            this.service.Setup(s => s.AddCategory(new AddCategoryBindingModel() { Name = "Somet" }))
                .Returns(false);
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AddCategory(new AddCategoryBindingModel() { Name = "Somet" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCategories"));
        }

        [TestMethod]
        public void RemoveCategory_ShouldRedirectTo_AllCategories_IfServiceRetursTrue()
        {
            this.service.Setup(s => s.RemoveCategory(new RemoveCategoryBindingModel() { Id = 1 }))
                .Returns(true);
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.RemoveCategory(new RemoveCategoryBindingModel() { Id = 1 }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCategories"));
        }

        [TestMethod]
        public void RemoveCategory_ShouldRedirectTo_AllCategories_IfServiceRetursFalse()
        {
            this.service.Setup(s => s.RemoveCategory(new RemoveCategoryBindingModel() { Id = 1 }))
                .Returns(false);
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.RemoveCategory(new RemoveCategoryBindingModel() { Id = 1 }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCategories"));
        }

        [TestMethod]
        public void MerchantDetails_ShouldRenderPartialView_MerchantDetails()
        {
            this.service.Setup(s => s.GetMerchant(1))
                .Returns(new MerchantDetailsViewModel());

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.MerchantDetails(1))
                .ShouldRenderPartialView("_MerchantDetails");
        }

        [TestMethod]
        public void DenyAccess_ShouldRedirectTo_AllMerchants_IfServiceReturnsTrue()
        {
            this.service.Setup(s => s.DenyMerchantAccess(new MerchantDenyAccessBindingModel() {Email = ""}))
                .Returns(true);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.DenyAccess(new MerchantDenyAccessBindingModel() { Email = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllMerchants"));
        }

        [TestMethod]
        public void DenyAccess_ShouldRedirectTo_AllMerchants_IfServiceReturnsFalse()
        {
            this.service.Setup(s => s.DenyMerchantAccess(new MerchantDenyAccessBindingModel() { Email = "" }))
                .Returns(false);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.DenyAccess(new MerchantDenyAccessBindingModel() { Email = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllMerchants"));
        }

        [TestMethod]
        public void AllowAccess_ShouldRedirectTo_AllPendingMerchants_IfServiceReturnsTrue()
        {
            this.service.Setup(s => s.AllowMerchantAccess(new MerchantAllowAccessBindingModel() { Id = 1}))
                .Returns(true);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllowAccess(new MerchantAllowAccessBindingModel() { Id = 1 }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllPendingMerchants"));
        }

        [TestMethod]
        public void AllowAccess_ShouldRedirectTo_AllPendingMerchants_IfServiceReturnsFalse()
        {
            this.service.Setup(s => s.AllowMerchantAccess(new MerchantAllowAccessBindingModel() { Id = 1 }))
                .Returns(false);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.AllowAccess(new MerchantAllowAccessBindingModel() { Id = 1 }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllPendingMerchants"));
        }

        [TestMethod]
        public void BannedCustomers_ShouldReturnPagedListOfBannedCustomers()
        {
            this.service.Setup(s => s.GetAllBannedCustomersPerPage(It.IsAny<int>()))
                .Returns(new PagedList<BannedCustomerViewModel>(new List<BannedCustomerViewModel>()
                {
                    new BannedCustomerViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.BannedCustomers(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<BannedCustomerViewModel>>();

        }

        [TestMethod]
        public void CustomerDetails_ShouldRenderPartialView_CustomerDetails()
        {
            this.service.Setup(s => s.GetCustomer(1))
                .Returns(new CustomerDetailsViewModel());

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.CustomerDetails(1))
                .ShouldRenderPartialView("_CustomerDetails");
        }

        [TestMethod]
        public void BanCustomer_ShouldRedirectTo_AllCustomers_IfServiceReturnsTrue()
        {
            this.service.Setup(s => s.IsCustomerBanned(new BanUnBanCustomerBindingModel() { Username = ""}))
                .Returns(true);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.BanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCustomers"));
        }

        [TestMethod]
        public void BanCustomer_ShouldRedirectTo_AllCustomers_IfServiceReturnsFalse()
        {
            this.service.Setup(s => s.IsCustomerBanned(new BanUnBanCustomerBindingModel() { Username = "" }))
                .Returns(false);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.BanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("AllCustomers"));
        }

        [TestMethod]
        public void UnBanCustomer_ShouldRedirectTo_BannedCustomers_IfServiceReturnsTrue()
        {
            this.service.Setup(s => s.UnBanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .Returns(true);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.UnBanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("BannedCustomers"));
        }

        [TestMethod]
        public void UnBanCustomer_ShouldRedirectTo_BannedCustomers_IfServiceReturnsFalse()
        {
            this.service.Setup(s => s.UnBanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .Returns(false);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.UnBanCustomer(new BanUnBanCustomerBindingModel() { Username = "" }))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("BannedCustomers"));
        }

        [TestMethod]
        public void PendingVauchers_ShouldReturnPagedListOfPendingVauchers()
        {
            this.service.Setup(s => s.GetPendingVauchers(It.IsAny<int>()))
                .Returns(new PagedList<PendingVaucherViewModel>(new List<PendingVaucherViewModel>()
                {
                    new PendingVaucherViewModel()
                }, 1, 9));

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.PendingVauchers(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<PendingVaucherViewModel>>();

        }

        [TestMethod]
        public void PendingVaucherImage_ShouldReturnFileContenResult()
        {
            this.service.Setup(s => s.GetVaucherImage(1))
                .Returns(new byte[3]);

            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.PendingVaucherImage(1))
                .ShouldRenderFileContents(new byte[3], "image/jpg");

        }

        [TestMethod]
        public void ActivateVaucher_ShouldRedirectTo_PendingVauchers()
        {
            var controller = new AdminController(this.service.Object);

            controller.WithCallTo(c => c.ActivateVaucher(new ActivateVaucherBindingModel()))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("PendingVauchers"));
        }
    }
}
