using System.Collections.Generic;

namespace SimpleStore.Models
{
    public interface ICartViewModel
    {

        List<ProductCart> Products { get; set; }

        bool SuccessPayment { get; set; }

        double Total { get; set; }

        void AddProduct(Product product);

        void RemoveProduct(Product product);
    }

}