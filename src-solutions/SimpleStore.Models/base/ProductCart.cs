using System;

namespace SimpleStore.Models
{
    public class ProductCart : IProduct, IProductCart
    {
        public ProductCart()
        {

        }

        public ProductCart(Product product)
        {
            Created = product.Created;
            Description = product.Description;
            EnabledUser = product.EnabledUser;
            Id = product.Id;
            ImageUrl = product.ImageUrl;
            Name = product.Name;
            Price = product.Price;
        }

        public int Qty { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public bool EnabledUser { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}