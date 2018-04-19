namespace VaucherSystem.Models.ViewModels.Merchant
{
    using System;
    using System.Collections.Generic;

    public class BoughtVaucherDetailsViewModel
    {
        public BoughtVaucherDetailsViewModel()
        {
            this.UniqueVaucherCode = new HashSet<string>();
        }
        public string Name { get; set; }
        public IEnumerable<bool> IsBought { get; set; }
        public ICollection<string> UniqueVaucherCode { get; set; }
        public IEnumerable<CustomersEmailUsernameViewModel> EmailsUsernames { get; set; }
    }
}
