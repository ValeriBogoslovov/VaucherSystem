namespace VaucherSystem.Models.ViewModels.Admin
{
    using System;

    public class PendingVaucherViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string MerchantName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public byte[] VaucherPicture { get; set; }
        public DateTime EndDate { get; set; }
    }
}
