namespace VaucherSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using Models.EntityModels.Identity;
    using Models.EntityModels;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using VaucherSystem.Data.Migrations;

    public class VaucherSystemDbContext : IdentityDbContext<User>
    {
        public VaucherSystemDbContext()
            : base("VaucherSystem", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VaucherSystemDbContext, Configuration>());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Vaucher> Vauchers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<CustomersVauchers> CustomersVauchers { get; set; }
        public DbSet<UniqueVaucherCode> UniqueVaucherCodes { get; set; }
        public static VaucherSystemDbContext Create()
        {
            return new VaucherSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
