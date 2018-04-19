namespace VaucherSystem.Services
{
    using Commons.Contracts;
    using AutoMapper;
    using Models.ViewModels.Account;
    using Models.EntityModels;
    using System.Collections.Generic;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Home;
    using Models.BindingModels.Merchant;
    using System;
    using Models.ViewModels.Merchant;

    public abstract class BaseService
    {
        protected IUnitOfWork db;
        protected IMapper mapper;
        protected MapperConfiguration configuration;
        public BaseService(IUnitOfWork db)
        {
            this.db = db;
            this.ConfigureMapper();
            this.CreateMapper();
        }

        private void CreateMapper()
        {
            this.mapper = configuration.CreateMapper();
        }
        private void ConfigureMapper()
        {
            configuration = new MapperConfiguration(m =>
            {
                m.CreateMap<MerchantRegisterViewModel, Merchant>();

                m.CreateMap<CustomerRegisterViewModel, Customer>();

                m.CreateMap<Merchant, MerchantViewModel>();

                m.CreateMap<Vaucher, VaucherViewModel>();

                //CHECK HOW TO MAP VIEWS WITH COLLECTION !!!!
                m.CreateMap<Customer, CustomerViewModel>();

                m.CreateMap<Customer, BannedCustomerViewModel>()
                .ForMember(bcvm => bcvm.Username, member => member.MapFrom(c=> c.AppUser.UserName));

                m.CreateMap<Category, CategoryViewModel>();
                m.CreateMap<Category, CheckboxCategoryViewModel>();

                m.CreateMap<CreateVaucherBindingModel, Vaucher>()
                .ForMember(v => v.CategoryId,
                member => member.MapFrom(cvbm => this.db.Categories.Find(cat => cat.Name == cvbm.CategoryName).Id));

                m.CreateMap<CreateUniqeVaucherCodesBindingModel, UniqueVaucherCode>();

                m.CreateMap<PictureBindingModel, Picture>();

                m.CreateMap<Picture, MerchantVaucherPictureViewModel>();

                m.CreateMap<Vaucher, MerchantVaucherViewModel>();
            });
        }
    }
}
