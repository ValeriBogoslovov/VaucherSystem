namespace VaucherSystem.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CustomersVauchers
    {
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; }

        [Key, Column(Order = 1)]
        public int VaucherId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Vaucher Vaucher { get; set; }
        public int BoughtVauchers { get; set; }
    }
}
