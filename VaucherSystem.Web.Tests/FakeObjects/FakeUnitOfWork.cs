namespace VaucherSystem.Web.Tests.FakeObjects
{
    using Commons.Contracts;
    using System;
    using Models.EntityModels.Identity;
    using Models.EntityModels;

    public class FakeUnitOfWork : IUnitOfWork
    {

        private IRepository<User> users;
        private IRepository<Customer> customers;
        private IRepository<Merchant> merchants;
        private IRepository<Category> categories;
        private IRepository<Vaucher> vauchers;
        private IRepository<Picture> pictures;
        private IRepository<CustomersVauchers> customersVauchers;
        private IRepository<UniqueVaucherCode> uniqueVaucherCodes;
        public IRepository<Category> Categories
        {
            get
            {
                return this.categories ?? (this.categories = new FakeRepo<Category>());
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return this.customers ?? (this.customers = new FakeRepo<Customer>());
            }
        }

        public IRepository<CustomersVauchers> CustomersVauchers
        {
            get
            {
                return this.customersVauchers ?? (this.customersVauchers = new FakeRepo<CustomersVauchers>());
            }
        }

        public IRepository<Merchant> Merchants
        {
            get
            {
                return this.merchants ?? (this.merchants = new FakeRepo<Merchant>());
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.pictures ?? (this.pictures = new FakeRepo<Picture>());
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.users ?? (this.users = new FakeRepo<User>());
            }
        }

        public IRepository<Vaucher> Vauchers
        {
            get
            {
                return this.vauchers ?? (this.vauchers = new FakeRepo<Vaucher>());
            }
        }

        public IRepository<UniqueVaucherCode> UniqueVaucherCodes
        {
            get
            {
                return this.uniqueVaucherCodes ?? (this.uniqueVaucherCodes = new FakeRepo<UniqueVaucherCode>());
            }
        }

        public void SaveChanges()
        {
        }
    }
}
