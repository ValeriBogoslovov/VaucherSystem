namespace VaucherSystem.Models.ViewModels.Customer
{
    using System;

    public class CustomerVaucherDetailsViewModel
    {
        public string UniqueCode { get; set; }
        public string MerchantName { get; set; }
        public string VaucherName { get; set; }
        public DateTime EndDate { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
