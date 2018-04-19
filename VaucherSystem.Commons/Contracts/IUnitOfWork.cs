namespace VaucherSystem.Commons.Contracts
{
    using System;
    using Models.EntityModels.Identity;
    using Models.EntityModels;

    public interface IUnitOfWork
    {
        void SaveChanges();
        IRepository<User> Users { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Merchant> Merchants { get; }
        IRepository<Vaucher> Vauchers { get; }
        IRepository<Picture> Pictures { get; }
        IRepository<CustomersVauchers> CustomersVauchers { get;}
        IRepository<UniqueVaucherCode> UniqueVaucherCodes { get; }
    }
}
