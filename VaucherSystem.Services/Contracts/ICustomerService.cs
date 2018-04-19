namespace VaucherSystem.Services.Contracts
{
    using Models.ViewModels.Customer;
    using System;
    using Models.BindingModels.Customer;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerVauchersViewModel> GetMyVauchers(string name);
        bool BuyVaucher(BuyVaucherBindingModel bm, string name);
        CustomerProfileViewModel MyProfile(string name);
        CustomerVaucherDetailsViewModel GetMyVaucherDetails(int id, string name);
    }
}
