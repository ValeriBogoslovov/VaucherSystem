namespace VaucherSystem.Models.EntityModels
{
    using System;

    public class UniqueVaucherCode
    {
        public int Id { get; set; }
        public int VaucherId { get; set; }
        public virtual Vaucher Vaucher { get; set; }
        public Guid UniqueCode { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public bool IsBought { get; set; }
    }
}
