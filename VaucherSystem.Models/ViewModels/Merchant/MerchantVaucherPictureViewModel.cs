namespace VaucherSystem.Models.ViewModels.Merchant
{
    using System;

    public class MerchantVaucherPictureViewModel
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public byte[] fileData { get; set; }
    }
}
