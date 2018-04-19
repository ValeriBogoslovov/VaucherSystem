namespace VaucherSystem.Models.ViewModels.Merchant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MerchantVaucherViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal DiscountedPrice { get; set; }
        public byte[] Pictures { get; set; }
        public DateTime EndDate { get; set; }
    }
}
