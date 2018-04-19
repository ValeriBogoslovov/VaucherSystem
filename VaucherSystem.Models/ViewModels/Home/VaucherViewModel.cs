namespace VaucherSystem.Models.ViewModels.Home
{
    using System;

    public class VaucherViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public byte[] Picture { get; set; }
        public DateTime EndDate { get; set; }

    }
}
