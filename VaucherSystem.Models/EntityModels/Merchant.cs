namespace VaucherSystem.Models.EntityModels
{
    using Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Merchant
    {
        public Merchant()
        {
            this.Categories = new HashSet<Category>();
            this.Vauchers = new HashSet<Vaucher>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ResponsiblePerson { get; set; }
        
        public string AppUserId { get; set; }
        
        public virtual User AppUser { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "The UIC must be 9 characters long.", MinimumLength = 9)]
        public string UIC { get; set; }
        public bool IsReal { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Vaucher> Vauchers { get; set; }
        public int SoldVauchers { get; set; }
    }
}