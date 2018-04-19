namespace VaucherSystem.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vaucher
    {
        public Vaucher()
        {
            this.Pictures = new HashSet<Picture>();
            this.CustomersVauchers = new HashSet<CustomersVauchers>();
            this.UniqueVaucherCode = new HashSet<UniqueVaucherCode>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal DiscountedPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<CustomersVauchers> CustomersVauchers { get; set; }

        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
        public bool IsTopVaucher { get; set; }
        public virtual ICollection<UniqueVaucherCode> UniqueVaucherCode { get; set; }
    }
}