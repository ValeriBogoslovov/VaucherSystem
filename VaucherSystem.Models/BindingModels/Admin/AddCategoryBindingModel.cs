namespace VaucherSystem.Models.BindingModels.Admin
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddCategoryBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
