namespace VaucherSystem.Models.ViewModels.Merchant
{
    using System;
    using System.Collections.Generic;

    public class VaucherDetailsViewModel
    {
        public VaucherDetailsViewModel()
        {
            this.Pictures = new HashSet<MerchantVaucherPictureViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public ICollection<MerchantVaucherPictureViewModel> Pictures { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}
