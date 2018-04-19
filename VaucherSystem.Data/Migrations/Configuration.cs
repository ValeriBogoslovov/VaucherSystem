namespace VaucherSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.EntityModels.Identity;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VaucherSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VaucherSystemDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
            }
            if (!context.Roles.Any(r => r.Name == "Customer"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Customer"));
            }
            if (!context.Roles.Any(r => r.Name == "Merchant"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Merchant"));
            }

            if (!context.Users.Any())
            {
                this.CreateAdminMerchantCustomer(context);
            }
            if (!context.Categories.Any())
            {
                this.CreateBasicCategories(context);
            }
            ////if (!context.Vauchers.Any())
            ////{
            ////    this.CreateTestVauchers(context);
            ////}

        }
        private void CreateTestVauchers(VaucherSystemDbContext context)
        {
            context.Vauchers.Add(new Vaucher()
            {
                Name = "Mostel",
                Quantity = 10,
                CategoryId = 6,
                IsActive = true,
                MerchantId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.Now.AddMonths(1),
                Description = "Ipsum50",
                Price = 90m,
                Discount = 20m,
                DiscountedPrice = this.GetDiscountedPrice(90m, 20m)
            });

        }

        private decimal GetDiscountedPrice(decimal price, decimal discount)
        {
            var discountAsPercentage = discount / 100;
            var realDiscount = discountAsPercentage * price;

            return price - realDiscount;
        }


        private void CreateBasicCategories(VaucherSystemDbContext context)
        {
            context.Categories.AddRange(new[]
            {
                new Category()
                {
                    Name = "Beauty"
                },
                new Category()
                {
                    Name = "Tourism"
                },
                new Category()
                {
                    Name = "Health"
                },
                new Category()
                {
                    Name = "Sport"
                },
                new Category()
                {
                    Name = "Massages"
                },
                new Category()
                {
                    Name = "Car service"
                },
                new Category()
                {
                    Name = "Spa and wellness"
                },
                new Category()
                {
                    Name = "Education"
                },
                new Category()
                {
                    Name = "Others"
                }
            });
        }

        private void CreateAdminMerchantCustomer(VaucherSystemDbContext context)
        {
            var adminEmail = "admin@admin.com";
            var adminUsername = adminEmail;
            var adminPassword = "Admin!234";
            var adminRole = "Administrator";

            var merchantName = "Hostel Mostel";
            var IsReal = true;
            var ResponsiblePerson = "Valeri Bogoslovov";
            var UIC = "BG8395602";
            var merchantUsername = merchantName.Replace(' ', '_');
            var merchantEmail = "hostelmostel@abv.bg";
            var merchantPassword = "9468856";
            var merchantRole = "Merchant";
            var merchantPhoneNumber = "+359888757677";

            var customerFirstname = "Petar";
            var customerLastname = "Georgiev";
            var customerPhoneNumber = "+359887900233";
            var customerUsername = "pesho_ludiq";
            var customerEmail = "pesho@abv.bg";
            var customerPassword = "pesho123";
            var customerRole = "Customer";
            var isBanned = false;

            var adminUser = new User()
            {
                UserName = adminUsername,
                Email = adminEmail
            };

            var merchantUser = new User()
            {
                UserName = merchantUsername,
                Email = merchantEmail,
                PhoneNumber = merchantPhoneNumber
            };

            var customerUser = new User()
            {
                UserName = customerUsername,
                Email = customerEmail,
                PhoneNumber = customerPhoneNumber
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var adminCreateResult = userManager.Create(adminUser, adminPassword);
            var merchantCreateResult = userManager.Create(merchantUser, merchantPassword);
            var customerCreateResult = userManager.Create(customerUser, customerPassword);

            if (!adminCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", adminCreateResult.Errors));
            }
            if (!merchantCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", merchantCreateResult.Errors));
            }
            if (!customerCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", customerCreateResult.Errors));
            }

            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
            var addMerchantRoleResult = userManager.AddToRole(merchantUser.Id, merchantRole);
            var addCustomerRoleResult = userManager.AddToRole(customerUser.Id, customerRole);

            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
            if (!addMerchantRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addMerchantRoleResult.Errors));
            }
            if (!addCustomerRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addCustomerRoleResult.Errors));
            }

            context.Merchants.Add(new Merchant
            {
                Name = merchantName,
                IsReal = IsReal,
                ResponsiblePerson = ResponsiblePerson,
                UIC = UIC,
                AppUser = userManager.FindByEmail(merchantEmail),
                SoldVauchers = 66,
            });

            context.Customers.Add(new Customer()
            {
                AppUser = userManager.FindByEmail(customerEmail),
                FirstName = customerFirstname,
                LastName = customerLastname,
                IsBanned = isBanned
            });
        }
    }
}
