using SimpleStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.ViewModels
{
    public class CartViewModel : Cart, ICartViewModel
    {
        public CartViewModel()
        {
            this.Products = new List<ProductCart>();
        }

        public CartViewModel(Cart shoppingCart)
        {
            Products = new List<ProductCart>();
            if (shoppingCart != null && shoppingCart.Products != null)
            {
                this.Products = shoppingCart.Products;
                this.Total = shoppingCart.Total;
            }
        }

        public CartViewModel(Product product)
        {
            Products = new List<ProductCart>();
            Products.Add(new ProductCart(product));
            this.Total += product.Price;
        }

        public void AddProduct(Product product)
        {
            var productFounded = this.Products
                    .Find(t => t.Id == product.Id);

            if (productFounded != null)
            {
                productFounded.Qty++;
            }
            else
            {
                this.Products.Add(new ProductCart(product));
            }

            this.Total += product.Price;
        }

        public void RemoveProduct(Product product)
        {
            this.Products.Remove(this.Products.FirstOrDefault(p => p.Id == product.Id));

            this.Total -= product.Price;
        }
    }
}