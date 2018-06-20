using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleStore.Models;
using SimpleStore.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Migrations
{

    [ExcludeFromCodeCoverageAttribute]
    public class SeedData
    {
        private SimpleStoreContext _context;
        private UserManager<SimpleStoreUser> _usermanager;
        private RoleManager<IdentityRole> _rolemanager;

        public SeedData(SimpleStoreContext context, 
            UserManager<SimpleStoreUser> usermanager,
            RoleManager<IdentityRole> rolemanager)
        {
            _context = context;
            _usermanager = usermanager;
            _rolemanager = rolemanager;

            _context.Database.EnsureCreated();
        }

        public async Task EnsureData()
        {
            await CreateUsersDummy();
            await createProductsDummy();
       
        }

        

        private async Task CreateUsersDummy()
        {
            if (await _usermanager.FindByEmailAsync(ConstantsSimpleStore.EMAIL_ADMIN_DEFAULT) == null)
            {

                await _rolemanager.CreateAsync(new IdentityRole(ConstantsSimpleStore.roleNameBackend));

                await _rolemanager.CreateAsync(new IdentityRole(ConstantsSimpleStore.roleNameCustomer));

                ///// Admin Default
                await CreateUser(ConstantsSimpleStore.EMAIL_ADMIN_DEFAULT, 
                    ConstantsSimpleStore.passwordadmin, 
                    ConstantsSimpleStore.roleNameBackend);

                ///// Customer Default
                await CreateUser(ConstantsSimpleStore.EMAIL_CUSTOMER_DEFAULT,
                    ConstantsSimpleStore.passwordadmin,
                    ConstantsSimpleStore.roleNameCustomer);

            }
        }

        private async Task CreateUser(string emailUser, string passwordUser, string userRole)
        {
            var user = new SimpleStoreUser()
            {
                UserName = emailUser,
                Email = emailUser
            };

            var resultuser = await _usermanager.CreateAsync(user, passwordUser);
            await _usermanager.AddToRoleAsync(user, userRole);
        }


        private async Task createProductsDummy()
        {
            if (!_context.Products.Any())
            {
                var product1 = new Product()
                {
                    Name = "Uno",
                    Description = "Uno 1",
                    Price = 1,
                    ImageUrl = "img/products/products-01.jpg"
                };

                var product2 = new Product()
                {
                    Name = "Dos",
                    Description = "Dos 2",
                    Price = 2,
                    ImageUrl = "img/products/products-02.jpg"
                };
                _context.Products.Add(product1);
                _context.Products.Add(product2);
                await _context.SaveChangesAsync();
            }
        }
    }
}
