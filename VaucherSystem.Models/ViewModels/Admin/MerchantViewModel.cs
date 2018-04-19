namespace VaucherSystem.Models.ViewModels.Admin
{
    using System;

    public class MerchantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string UIC { get; set; }
        public bool IsReal { get; set; }
    }
}
