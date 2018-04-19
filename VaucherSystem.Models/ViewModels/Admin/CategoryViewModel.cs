namespace VaucherSystem.Models.ViewModels.Admin
{
    using Home;
    using System;
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VaucherViewModel> Vauchers { get; set; }
        public IEnumerable<MerchantViewModel> Merchants { get; set; }
    }
}
