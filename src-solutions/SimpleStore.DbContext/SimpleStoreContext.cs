using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    public class SimpleStoreContext : IdentityDbContext<SimpleStoreUser>
    {
        public SimpleStoreContext(DbContextOptions<SimpleStoreContext> options)
            : base(options)
        {
        }
        public SimpleStoreContext()
        { }
        public virtual DbSet<SimpleStoreUser> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductCategory> ProductsCategories { get; set; }
        public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }
    }
}