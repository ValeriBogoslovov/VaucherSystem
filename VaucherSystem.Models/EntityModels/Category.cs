namespace VaucherSystem.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Merchants = new HashSet<Merchant>();
            this.Vauchers = new HashSet<Vaucher>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Merchant> Merchants { get; set; }
        public virtual ICollection<Vaucher> Vauchers { get; set; }
    }
}