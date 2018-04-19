namespace VaucherSystem.Services.Contracts
{
    using Models.ViewModels.Admin;
    using PagedList;
    using Models.BindingModels.Admin;

    public interface IAdminService
    {
        IPagedList<MerchantViewModel> GetPendingMerchantsPerPage(int page);
        IPagedList<MerchantViewModel> GetAllMerchantsPerPage(int page);
        IPagedList<CustomerViewModel> GetAllCustomersPerPage(int page);
        IPagedList<BannedCustomerViewModel> GetAllBannedCustomersPerPage(int page);
        IPagedList<CategoryViewModel> GetAllCategoriesPerPage(int page);
        bool AddCategory(AddCategoryBindingModel bm);
        bool RemoveCategory(RemoveCategoryBindingModel bm);
        MerchantDetailsViewModel GetMerchant(int id);
        bool DenyMerchantAccess(MerchantDenyAccessBindingModel bm);
        bool AllowMerchantAccess(MerchantAllowAccessBindingModel bm);
        CustomerDetailsViewModel GetCustomer(int id);
        bool IsCustomerBanned(BanUnBanCustomerBindingModel bm);
        bool UnBanCustomer(BanUnBanCustomerBindingModel bm);
        IPagedList<PendingVaucherViewModel> GetPendingVauchers(int page);
        byte[] GetVaucherImage(int vaucherId);
        void ActivateVaucher(ActivateVaucherBindingModel bm);
    }
}
