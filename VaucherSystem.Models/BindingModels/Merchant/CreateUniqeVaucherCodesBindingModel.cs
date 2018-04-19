namespace VaucherSystem.Models.BindingModels.Merchant
{
    using System;

    public class CreateUniqeVaucherCodesBindingModel
    {
        public int VaucherId { get; set; }
        public Guid UniqueCode { get; set; }
        public bool IsBought { get; set; }
    }
}
