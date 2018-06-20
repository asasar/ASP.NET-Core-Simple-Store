using Microsoft.AspNetCore.Identity;
using SimpleStore.Models;
using SimpleStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Tests.DummyData
{
    public static class ProductsDummy
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id=1,
                    Description = "Hamlet",
                    Name = "William Shakespeare",
                    Created = DateTime.Now,
                    Price = new Random().Next(100),
                    ImageUrl = new Guid().ToString()
                },
                new Product
                {
                    Id =2,
                    Description = "A Midsummer Night's Dream",
                    Name = "William Shakespeare",
                    Created = DateTime.Now,
                    Price = new Random().Next(100),
                    ImageUrl = new Guid().ToString()
                }
            };
        }

        public static List<ProductCart> GetProductCart()
        {
            return new List<ProductCart>()
            {
                new ProductCart
                {
                    Id=1,
                    Description = "Hamlet",
                    Name = "William Shakespeare",
                    Created = DateTime.Now,
                    Price = new Random().Next(100),
                    ImageUrl = new Guid().ToString(),
                    Qty = new Random().Next(100)
                },
                new ProductCart
                {
                    Id =2,
                    Description = "A Midsummer Night's Dream",
                    Name = "William Shakespeare",
                    Created = DateTime.Now,
                    Price = new Random().Next(100),
                    ImageUrl = new Guid().ToString(),
                    Qty = new Random().Next(100)
                }
            };
        }

        public static List<SimpleStoreUser> GetUsers()
        {
            return new List<SimpleStoreUser>()
            {
                new SimpleStoreUser
                {
                    UserName = Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString()
                },
                new SimpleStoreUser
                {
                    UserName = Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString()
                }
            };
        }


        

    }
}
