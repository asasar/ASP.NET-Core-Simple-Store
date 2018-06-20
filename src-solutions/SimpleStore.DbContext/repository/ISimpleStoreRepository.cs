
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    public interface ISimpleStoreRepository
    {
        IEnumerable<IProduct> GetAllProducts();

        void AddProduct(IProduct newProduct);

        Cart BuyNow(Cart shoppingCart, string userEmail);

        Task<bool> SaveChangesAsync();

        Product GetProductById(string id);
    }
}