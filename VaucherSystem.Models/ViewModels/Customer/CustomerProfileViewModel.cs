namespace VaucherSystem.Models.ViewModels.Customer
{
    using System;

    public class CustomerProfileViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int BoughtVauchers { get; set; }
    }
}
