namespace VaucherSystem.Commons
{
    using Contracts;
    using Models.EntityModels;
    using Models.EntityModels.Identity;
    using System;
    using Data;

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<User> users;
        private IRepository<Customer> customers;
        private IRepository<Merchant> merchants;
        private IRepository<Category> categories;
        private IRepository<Vaucher> vauchers;
        private IRepository<Picture> pictures;
        private IRepository<CustomersVauchers> customersVauchers;
        private IRepository<UniqueVaucherCode> uniqueVaucherCodes;

        private VaucherSystemDbContext context;
        public UnitOfWork()
        {
            this.context = new VaucherSystemDbContext();
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.pictures ?? (this.pictures = new Repository<Picture>(context));
            }
        }
        public IRepository<Customer> Customers
        {
            get
            {
                return this.customers ?? (this.customers = new Repository<Customer>(context));
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.users ?? (this.users = new Repository<User>(context));
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.categories ?? (this.categories = new Repository<Category>(context));
            }
        }

        public IRepository<Merchant> Merchants
        {
            get
            {
                return this.merchants ?? (this.merchants = new Repository<Merchant>(context));
            }
        }

        public IRepository<Vaucher> Vauchers
        {
            get
            {
                return this.vauchers ?? (this.vauchers = new Repository<Vaucher>(context));
            }
        }

        public IRepository<CustomersVauchers> CustomersVauchers
        {
            get
            {
                return this.customersVauchers ?? (this.customersVauchers = new Repository<CustomersVauchers>(context));
            }
        }

        public IRepository<UniqueVaucherCode> UniqueVaucherCodes
        {
            get
            {
                return this.uniqueVaucherCodes ?? (this.uniqueVaucherCodes = new Repository<UniqueVaucherCode>(context));
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
