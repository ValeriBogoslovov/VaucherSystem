namespace VaucherSystem.Services.Contracts
{
    using Models.ViewModels.Merchant;
    using PagedList;
    using System.Collections.Generic;
    using Models.BindingModels.Merchant;

    public interface IMerchantService
    {
        IPagedList<MerchantVaucherViewModel> GetMyActiveVauchersPerPage(int page, string name);
        IPagedList<MerchantVaucherViewModel> GetMyPassedVauchersPerPage(int page, string name);
        CategoryNameViewModel GetCategories();
        void CreateVaucher(CreateVaucherBindingModel bm, string name);
        byte[] GetPicture(int vaucherId);
        VaucherDetailsViewModel GetVaucherDetails(int vaucherId);
        IEnumerable<MerchantVaucherPictureViewModel> GetPictures(int vaucherId);
        void RemoveVaucher(RemoveVaucherBindingModel bm);
        IEnumerable<BoughtVauchersViewModel> GetMyBoughtVauchers(string name);
        BoughtVaucherDetailsViewModel GetBoughtVaucherDetails(int id);
    }
}
