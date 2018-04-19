namespace VaucherSystem.Services
{
    using Contracts;
    using Commons.Contracts;
    using PagedList;
    using Models.ViewModels.Home;
    using System;
    using System.Linq;
    using Models.EntityModels;
    using System.Collections.Generic;
    using Models.ViewModels.Merchant;
    using Models.BindingModels.Merchant;
    using System.Web;
    using System.IO;

    public class MerchantService : BaseService, IMerchantService
    {
        private const int CountPerPage = 9;

        public MerchantService(IUnitOfWork db) : base(db)
        {
        }

        public CategoryNameViewModel GetCategories()
        {
            return new CategoryNameViewModel()
            {
                CategoryName = this.db.Categories.GetAll().Select(cat => cat.Name)
            };
        }

        public IPagedList<MerchantVaucherViewModel> GetMyActiveVauchersPerPage(int page, string name)
        {
            var vauchers = this.db.Vauchers
                .FindMany(v => v.Merchant.AppUser.UserName == name && v.EndDate > DateTime.Now && v.IsActive == true)
                .OrderBy(v => v.EndDate)
                .ToList()
                .Select(v => new MerchantVaucherViewModel()
                {
                    Id = v.Id,
                    EndDate = v.EndDate,
                    Price = v.Price,
                    Discount = v.Discount,
                    DiscountedPrice = v.DiscountedPrice,
                    Name = v.Name,
                    Quantity = v.Quantity,
                    Pictures = this.db.Pictures.Find(p=> p.VaucherId == v.Id).FileData
                }).ToPagedList(page, CountPerPage);

            return vauchers;
        }

        public IPagedList<MerchantVaucherViewModel> GetMyPassedVauchersPerPage(int page, string name)
        {
            var vauchers = this.db.Vauchers
                .FindMany(v => (v.Merchant.AppUser.UserName == name && v.IsActive == false) || v.EndDate < DateTime.Now)
                .OrderBy(v => v.EndDate)
                .ToList()
                .Select(v => new MerchantVaucherViewModel()
                {
                    Id = v.Id,
                    EndDate = v.EndDate,
                    Price = v.Price,
                    Discount = v.Discount,
                    DiscountedPrice = v.DiscountedPrice,
                    Name = v.Name,
                    Quantity = v.Quantity,
                    Pictures = this.db.Pictures.Find(p => p.VaucherId == v.Id).FileData
                }).ToPagedList(page, CountPerPage);

            return vauchers;
        }

        public void CreateVaucher(CreateVaucherBindingModel bm, string name)
        {
            bm.DiscountedPrice = this.GetDiscountedPrice(bm);
            bm.Pictures = this.convertToByteArray(bm.Files);

            for (int i = 0; i < bm.Quantity; i++)
            {
                bm.UniqueVaucherCode.Add(new CreateUniqeVaucherCodesBindingModel()
                {
                    UniqueCode = Guid.NewGuid(),
                    IsBought = false
                });
            }

            this.db.Merchants.Find(m => m.AppUser.UserName == name)
                .Vauchers
                .Add(this.mapper.Map<CreateVaucherBindingModel, Vaucher>(bm));

            this.db.SaveChanges();
        }

        private ICollection<PictureBindingModel> convertToByteArray(IEnumerable<HttpPostedFileBase> files)
        {
            ICollection<PictureBindingModel> fileData = new List<PictureBindingModel>();
            foreach (var file in files)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData.Add(new PictureBindingModel()
                    {
                        FileData = binaryReader.ReadBytes(file.ContentLength)
                    });
                }
            }

            return fileData;
        }

        private decimal GetDiscountedPrice(CreateVaucherBindingModel bm)
        {
            var discountAsPercentage = bm.Discount / 100;
            var realDiscount = discountAsPercentage * bm.Price;

            return bm.Price - realDiscount;
        }

        public byte[] GetPicture(int vaucherId)
        {
            var pic = this.db.Pictures.Find(p => p.VaucherId == vaucherId).FileData;
            return pic;
        }

        public VaucherDetailsViewModel GetVaucherDetails(int vaucherId)
        {
            var vaucherPics = this.db.Pictures.FindMany(p => p.VaucherId == vaucherId)
                .Select(p => new MerchantVaucherPictureViewModel()
                {
                    fileData = p.FileData,
                    Id = p.Id
                })
                .ToList();

            var vaucher = this.db.Vauchers.Find(v => v.Id == vaucherId);

            return new VaucherDetailsViewModel()
            {
                Id = vaucher.Id,
                Description = vaucher.Description,
                Discount = vaucher.Discount,
                DiscountedPrice = vaucher.DiscountedPrice,
                Price = vaucher.Price,
                StartDate = vaucher.StartDate,
                EndDate = vaucher.EndDate,
                Name = vaucher.Name,
                Quantity = vaucher.Quantity,
                Pictures = vaucherPics,
                CategoryName = vaucher.Category.Name
            };

        }

        public IEnumerable<MerchantVaucherPictureViewModel> GetPictures(int vaucherId)
        {
            var pics = this.db.Pictures.FindMany(p => p.VaucherId == vaucherId).ToList()
                .Select(p => new MerchantVaucherPictureViewModel()
                {
                    fileData = p.FileData,
                    Id = p.Id
                });

            return pics;
        }

        public void RemoveVaucher(RemoveVaucherBindingModel bm)
        {
            var voucher = this.db.Vauchers.Find(v => v.Id == bm.Id);
            voucher.IsActive = false;
            this.db.SaveChanges();
        }

        public IEnumerable<BoughtVauchersViewModel> GetMyBoughtVauchers(string name)
        {
            var merchant = this.db.Merchants.Find(m => m.AppUser.UserName == name);
            var model = this.db.Vauchers.GetAll()
                .Where(v => v.MerchantId == merchant.Id)
                .ToList()
                .Select(v => new BoughtVauchersViewModel()
                {
                    Id = v.Id,
                    Name = v.Name
                });

            return model;
        }

        public BoughtVaucherDetailsViewModel GetBoughtVaucherDetails(int id)
        {
            var vaucher = this.db.Vauchers.Find(v => v.Id == id);
            var uniqueVaucherCode = this.db.UniqueVaucherCodes.FindMany(uvc => uvc.VaucherId == vaucher.Id).ToList();

            var model = new BoughtVaucherDetailsViewModel()
            {
                Name = vaucher.Name,
                IsBought = uniqueVaucherCode.Select(uvc => uvc.IsBought),
                UniqueVaucherCode = uniqueVaucherCode.Select(uvc => uvc.UniqueCode.ToString()).ToList(),
                EmailsUsernames = vaucher.CustomersVauchers.Select(cv => new CustomersEmailUsernameViewModel()
                        {
                            Email = cv.Customer.AppUser.Email,
                            Username = cv.Customer.AppUser.UserName
                        })
            };

            return model;
        }
    }
}
