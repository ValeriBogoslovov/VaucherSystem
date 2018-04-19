namespace VaucherSystem.Services
{
    using Contracts;
    using System;
    using Commons.Contracts;
    using System.Collections.Generic;
    using Models.ViewModels.Home;
    using System.Linq;
    using PagedList;
    using Models.EntityModels;

    public class HomeService : BaseService, IHomeService
    {
        private static int vauchersPerPage = 9;
        public HomeService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<CheckboxCategoryViewModel> AllCategories()
        {
            return this.db.Categories.GetAll()
                .Select(this.mapper.Map<Category, CheckboxCategoryViewModel>).ToList();
        }

        public IPagedList<VaucherViewModel> GetAllVauchers(int page)
        {
            var vauchers = this.db.Vauchers.GetAll()
                .Where(v => v.EndDate > DateTime.Now && v.Quantity > 0 && v.Merchant.IsReal == true && v.IsActive == true)
                .OrderBy(v=> v.Name)
                .Select(v => new VaucherViewModel()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Price = v.Price,
                    Discount = v.Discount,
                    DiscountedPrice = v.DiscountedPrice,
                    Quantity = v.Quantity,
                    EndDate = v.EndDate,
                    Picture = v.Pictures.FirstOrDefault(p => p.VaucherId == v.Id).FileData
                }).ToPagedList(page, vauchersPerPage);
            
            return vauchers;
        }

        public byte[] GetPicture(int vaucherId)
        {
            var pic = this.db.Pictures.Find(p => p.VaucherId == vaucherId).FileData;
            return pic;
        }

        public VaucherViewModel GetTopVaucher()
        {
            var vaucherDiscount = this.db.Vauchers.GetAll()
                .Where(v => v.IsActive == true && v.Quantity > 0)
                .Select(v => v.Discount)
                .Max();
            
            var vaucher = this.db.Vauchers.Find(v => v.Discount == vaucherDiscount);
            return new VaucherViewModel()
                {
                    Id = vaucher.Id,
                    Discount = vaucher.Discount,
                    DiscountedPrice = vaucher.DiscountedPrice,
                    EndDate = vaucher.EndDate,
                    Name = vaucher.Name,
                    Quantity = vaucher.Quantity,
                    Price = vaucher.Price,
                    Picture = vaucher.Pictures.FirstOrDefault(p => p.VaucherId == vaucher.Id).FileData
                };
        }

        public IndexVaucherDetailsViewModel GetVaucherDetails(int vaucherId)
        {

            var vaucher = this.db.Vauchers.Find(v => v.Id == vaucherId);
            return new IndexVaucherDetailsViewModel()
            {
                Id = vaucher.Id,
                Description = vaucher.Description,
                Discount = vaucher.Discount,
                DiscountedPrice = vaucher.DiscountedPrice,
                Price = vaucher.Price,
                EndDate = vaucher.EndDate,
                Name = vaucher.Name,
                Quantity = vaucher.Quantity,
                Picture = this.db.Pictures.Find(p => p.VaucherId == vaucherId).FileData,
                CategoryName = vaucher.Category.Name
            };
        }
    }
}
