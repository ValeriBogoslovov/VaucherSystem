namespace VaucherSystem.Models.EntityModels
{
    using Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Customer()
        {
            this.CustomersVauchers = new HashSet<CustomersVauchers>();
        }
        public int Id { get; set; }

        [Required]
        public string AppUserId { get; set; }
        
        public virtual User AppUser { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required]
        public bool IsBanned { get; set; }

        public virtual ICollection<CustomersVauchers>CustomersVauchers { get; set; }
    }
}
