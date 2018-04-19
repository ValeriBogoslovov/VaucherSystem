namespace VaucherSystem.Models.ViewModels.Home
{
    using System;

    public class IndexVaucherDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public byte[] Picture { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}
