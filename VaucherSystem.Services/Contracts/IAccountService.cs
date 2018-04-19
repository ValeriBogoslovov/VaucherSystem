namespace VaucherSystem.Services.Contracts
{
    using Models.ViewModels.Account;
    using Models.EntityModels.Identity;

    public interface IAccountService
    {
        void RegisterCustomer(string id, CustomerRegisterViewModel model);
        void RegisterMerchant(string id, MerchantRegisterViewModel model);
        string CheckIfUserExists(User user);
    }
}
