namespace VaucherSystem.Services
{
    using Contracts;
    using Models.ViewModels.Account;
    using Commons.Contracts;
    using Models.EntityModels;
    using System;
    using Models.EntityModels.Identity;

    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnitOfWork db) : base(db)
        {
        }

        public string CheckIfUserExists(User user)
        {
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.UserName;
            }
        }

        public void RegisterCustomer(string id, CustomerRegisterViewModel model)
        {
            var customer = this.mapper.Map<CustomerRegisterViewModel, Customer>(model);
            customer.AppUserId = id;

            this.db.Customers.Add(customer);

            this.db.SaveChanges();
        }

        public void RegisterMerchant(string id, MerchantRegisterViewModel model)
        {
            var merchant = this.mapper.Map<MerchantRegisterViewModel, Merchant>(model);
            merchant.AppUserId = id;

            this.db.Merchants.Add(merchant);

            this.db.SaveChanges();
        }
    }
}
