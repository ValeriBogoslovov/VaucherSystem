namespace VaucherSystem.Services
{
    using Contracts;
    using Models.ViewModels.Admin;
    using Commons.Contracts;
    using System.Linq;
    using Models.EntityModels;
    using PagedList;
    using System;
    using Models.BindingModels.Admin; 
    public class AdminService : BaseService, IAdminService
    {
        private const int CountPerPage = 9;
        public AdminService(IUnitOfWork db) : base(db)
        {
        }

        public bool AddCategory(AddCategoryBindingModel bm)
        {
            if (!this.db.Categories.GetAll().Any(c=> c.Name == bm.Name))
            {
                this.db.Categories.Add(new Category()
                {
                    Name = bm.Name
                });
                this.db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DenyMerchantAccess(MerchantDenyAccessBindingModel bm)
        {
            var merchant = this.db.Merchants.Find(m => m.AppUser.Email == bm.Email);

            if (merchant == null)
            {
                return false;
            }

            merchant.IsReal = false;
            this.db.SaveChanges();
            return true;
        }
        public bool AllowMerchantAccess(MerchantAllowAccessBindingModel bm)
        {
            var merchant = this.db.Merchants.Find(m => m.Id == bm.Id);

            if (merchant == null)
            {
                return false;
            }

            merchant.IsReal = true;
            this.db.SaveChanges();
            return true;
        }

        public IPagedList<CategoryViewModel> GetAllCategoriesPerPage(int page)
        {
            return this.db.Categories.GetAll()
                .OrderBy(c => c.Id)
                .Select(this.mapper.Map<Category, CategoryViewModel>)
                .ToPagedList(page, CountPerPage);
        }

        public IPagedList<CustomerViewModel> GetAllCustomersPerPage(int page)
        {
            return this.db.Customers.GetAll()
                .Where(c => c.IsBanned == false)
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .Select(this.mapper.Map<Customer, CustomerViewModel>)
                .ToPagedList(page, CountPerPage);
        }

        public IPagedList<MerchantViewModel> GetAllMerchantsPerPage(int page)
        {
            return this.db.Merchants.GetAll()
                .Where(m => m.IsReal == true)
                .OrderBy(m => m.Name)
                .Select(this.mapper.Map<Merchant, MerchantViewModel>)
                .ToPagedList(page, CountPerPage);
        }

        public MerchantDetailsViewModel GetMerchant(int id)
        {
            var merchant = this.db.Merchants.Find(m => m.Id == id);
            return new MerchantDetailsViewModel()
            {
                Email = merchant.AppUser.Email,
                PhoneNumber = merchant.AppUser.PhoneNumber,
                SoldVauchers = merchant.SoldVauchers
            };
        }

        public IPagedList<MerchantViewModel> GetPendingMerchantsPerPage(int page)
        {
            return this.db.Merchants.GetAll()
                .Where(m => m.IsReal == false)
                .OrderBy(m => m.Name)
                .Select(this.mapper.Map<Merchant, MerchantViewModel>)
                .ToPagedList(page, CountPerPage);
        }

        public bool RemoveCategory(RemoveCategoryBindingModel bm)
        {
            if (this.db.Categories.GetAll().Any(c=> c.Id == bm.Id))
            {
                var category = this.db.Categories.Find(c => c.Id == bm.Id);
                this.db.Categories.Remove(category);
                this.db.SaveChanges();
                return true;
            }
            return false;
        }

        public CustomerDetailsViewModel GetCustomer(int id)
        {
            var customer = this.db.Customers.Find(c => c.Id == id);
            var boughtVauchers = this.db.CustomersVauchers.FindMany(c => c.CustomerId == id)
                .Sum(x => x.BoughtVauchers);

            return new CustomerDetailsViewModel()
            {
                Email = customer.AppUser.Email,
                PhoneNumber = customer.AppUser.PhoneNumber,
                Username = customer.AppUser.UserName,
                BoughtVauchers = boughtVauchers
            };
        }

        public bool IsCustomerBanned(BanUnBanCustomerBindingModel bm)
        {
            var customer = this.db.Customers.Find(c => c.AppUser.UserName == bm.Username);

            if (customer == null)
            {
                return false;
            }

            customer.IsBanned = true;
            this.db.SaveChanges();
            return true;
        }

        public IPagedList<BannedCustomerViewModel> GetAllBannedCustomersPerPage(int page)
        {
            return this.db.Customers.GetAll()
                .Where(c => c.IsBanned == true)
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .Select(this.mapper.Map<Customer, BannedCustomerViewModel>)
                .ToPagedList(page, CountPerPage);
        }

        public bool UnBanCustomer(BanUnBanCustomerBindingModel bm)
        {
            var customer = this.db.Customers.Find(c => c.AppUser.UserName == bm.Username);

            if (customer == null)
            {
                return false;
            }

            customer.IsBanned = false;
            this.db.SaveChanges();
            return true;
        }

        public IPagedList<PendingVaucherViewModel> GetPendingVauchers(int page)
        {
            return this.db.Vauchers.GetAll()
                .Where(v => v.IsActive == false)
                .OrderBy(v => v.Name)
                .ToList()
                .Select(v => new PendingVaucherViewModel()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Quantity = v.Quantity,
                    Price = v.Price,
                    Discount = v.Discount,
                    DiscountedPrice = v.DiscountedPrice,
                    MerchantName = v.Merchant.Name,
                    EndDate = v.EndDate,
                    VaucherPicture = v.Pictures.FirstOrDefault(p => p.VaucherId == v.Id).FileData 
                }).ToPagedList(page, CountPerPage);
        }

        public byte[] GetVaucherImage(int vaucherId)
        {
            var vaucherImage = this.db.Pictures.Find(p => p.VaucherId == vaucherId).FileData;
            return vaucherImage;
        }

        public void ActivateVaucher(ActivateVaucherBindingModel bm)
        {
            var vaucher = this.db.Vauchers.Find(v => v.Id == bm.Id);
            vaucher.IsActive = true;
            this.db.SaveChanges();
        }
    }
}
