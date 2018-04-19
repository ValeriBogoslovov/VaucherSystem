namespace VaucherSystem.Services
{
    using Contracts;
    using System;
    using Commons.Contracts;
    using Models.ViewModels.Customer;
    using Models.BindingModels.Customer;
    using Models.EntityModels;
    using System.Linq;
    using System.Collections.Generic;

    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWork db) : base(db)
        {
        }

        public bool BuyVaucher(BuyVaucherBindingModel bm, string name)
        {
            if (bm.Agrees != true)
            {
                return false;
            }
            var vaucher = this.db.Vauchers.Find(v => v.Id == bm.Id);
            
            vaucher.Quantity--;
            if (vaucher.Quantity == 0)
            {
                vaucher.IsActive = false;
            }
            var customer = this.db.Customers.Find(c => c.AppUser.UserName == name);

            var vaucherUniqueCode = vaucher.UniqueVaucherCode.FirstOrDefault(uvc => uvc.IsBought == false);

            vaucherUniqueCode.CustomerId = customer.Id;
            vaucherUniqueCode.IsBought = true;

            if (!this.db.CustomersVauchers.GetAll().Any(cv => cv.CustomerId == customer.Id && cv.VaucherId == vaucher.Id))
            {
                this.db.Customers.Find(c => c.AppUser.UserName == name)
                .CustomersVauchers.Add(new CustomersVauchers()
                {
                    VaucherId = bm.Id,
                    BoughtVauchers = 1
                });
            }
            else
            {
                this.db.CustomersVauchers.Find(cv => cv.CustomerId == customer.Id).BoughtVauchers++;
            }
            vaucher.Merchant.SoldVauchers++;

            this.db.SaveChanges();
            return true;
        }

        public CustomerVaucherDetailsViewModel GetMyVaucherDetails(int id, string name)
        {
            var vaucher = this.db.Vauchers.Find(v => v.Id == id);
            var customer = this.db.Customers.Find(c => c.AppUser.UserName == name);

            return new CustomerVaucherDetailsViewModel()
            {
                CustomerEmail = customer.AppUser.Email,
                CustomerFirstname = customer.FirstName,
                CustomerLastname = customer.LastName,
                MerchantName = vaucher.Merchant.Name,
                VaucherName = vaucher.Name,
                EndDate = vaucher.EndDate,
                DiscountedPrice = vaucher.DiscountedPrice,
                UniqueCode = vaucher.UniqueVaucherCode
                                    .FirstOrDefault(uvc => uvc.CustomerId == customer.Id)
                                    .UniqueCode.ToString()
            };
        }

        public IEnumerable<CustomerVauchersViewModel> GetMyVauchers(string name)
        {
            var customerVauchers = this.db.Customers
                .Find(c => c.AppUser.UserName == name)
                .CustomersVauchers
                .Select(cv => cv.Vaucher)
                .Select(cv => new CustomerVauchersViewModel()
                {
                    Id = cv.Id,
                    VaucherName = cv.Name,
                    EndDate = cv.EndDate
                });

            return customerVauchers;
        }

        public BuyVaucherViewModel GetVaucherToBuy(int vaucherId)
        {
            var vaucher = this.db.Vauchers.Find(v => v.Id == vaucherId);

            return new BuyVaucherViewModel()
            {
                Id = vaucher.Id,
                Name = vaucher.Name,
                DiscountedPrice = vaucher.DiscountedPrice,
                Quantity = vaucher.Quantity
            };
        }

        public CustomerProfileViewModel MyProfile(string name)
        {
            var customer = this.db.Customers.Find(c => c.AppUser.UserName == name);
            return new CustomerProfileViewModel()
            {
                Id = customer.Id,
                Email = customer.AppUser.Email,
                Username = customer.AppUser.UserName,
                BoughtVauchers = customer.CustomersVauchers.Select(cv => cv.BoughtVauchers).Sum()
            };
        }
    }
}
