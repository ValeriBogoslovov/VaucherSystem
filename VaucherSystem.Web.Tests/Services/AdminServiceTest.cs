namespace VaucherSystem.Web.Tests.Services
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FakeObjects;
    using VaucherSystem.Services.Contracts;
    using Commons.Contracts;
    using VaucherSystem.Services;
    using Models.EntityModels;
    using System.Linq;
    using Models.BindingModels.Admin;
    using Models.EntityModels.Identity;

    [TestClass]
    public class AdminServiceTest
    {
        private IUnitOfWork fakeDb;
        private IAdminService service;

        [TestInitialize]
        public void Initialize()
        {
            this.fakeDb = new FakeUnitOfWork();
            this.service = new AdminService(fakeDb);
        }

        [TestMethod]
        public void AddCategoryTest()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Spa"
            };
            this.fakeDb.Categories.Add(category);

            Assert.AreEqual(1, this.fakeDb.Categories.Find(c => c.Id == category.Id).Id);
        }

        [TestMethod]
        public void DenyMerchantAccessTest()
        {
            var email = "testc@abv.bg";
            var appuser = new User()
            {
                Email = email
            };

            this.fakeDb.Merchants.Add(new Merchant()
            {
                AppUser = appuser,
                Id = 1,
                Name = "Test",
                UIC = "BG5698745",
                ResponsiblePerson = "Vasko Keca"
            });

            Assert.AreEqual(email, this.fakeDb.Merchants.Find(m => m.AppUser.Email == appuser.Email).AppUser.Email);
        }
    }
}
