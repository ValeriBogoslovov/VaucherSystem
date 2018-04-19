namespace VaucherSystem.Models.ViewModels.Customer
{
    using System;

    public class BuyVaucherViewModel
    {
        public int Id { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}
