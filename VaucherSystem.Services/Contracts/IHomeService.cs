namespace VaucherSystem.Services.Contracts
{
    using Models.ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using PagedList;

    public interface IHomeService
    {
        IPagedList<VaucherViewModel> GetAllVauchers(int page);
        byte[] GetPicture(int vaucherId);
        IndexVaucherDetailsViewModel GetVaucherDetails(int vaucherId);
        VaucherViewModel GetTopVaucher();
        IEnumerable<CheckboxCategoryViewModel> AllCategories();
    }
}
