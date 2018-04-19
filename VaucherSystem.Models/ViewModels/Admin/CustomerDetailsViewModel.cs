namespace VaucherSystem.Models.ViewModels.Admin
{
    using System;

    public class CustomerDetailsViewModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int BoughtVauchers { get; set; }
        public string Username { get; set; }
    }
}
