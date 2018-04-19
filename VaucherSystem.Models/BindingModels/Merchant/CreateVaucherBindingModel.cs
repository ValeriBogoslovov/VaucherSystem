namespace VaucherSystem.Models.BindingModels.Merchant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CreateVaucherBindingModel
    {
        public CreateVaucherBindingModel()
        {
            this.UniqueVaucherCode = new HashSet<CreateUniqeVaucherCodesBindingModel>();
        }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]

        public int Quantity { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime StartDate
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal DiscountedPrice { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Choose file")]
        public HttpPostedFileBase[] Files { get; set; }
        public ICollection<PictureBindingModel> Pictures { get; set; }
        public int MerchantId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<CreateUniqeVaucherCodesBindingModel> UniqueVaucherCode { get; set; }
    }
}
