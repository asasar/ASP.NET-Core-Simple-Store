using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    public class SimpleStoreRepository : ISimpleStoreRepository
    {
        private SimpleStoreContext _context;
        private ILogger<SimpleStoreRepository> _logger;
        public SimpleStoreRepository(SimpleStoreContext context, ILogger<SimpleStoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddProduct(IProduct newProduct)
        {
            _context.Add(newProduct);
        }
        public Product GetProductById(string id)
        {
            _logger.LogInformation("Get list Products");
            return _context.Products
                .Where(t => t.Id == Convert.ToInt64(id))
                .SingleOrDefault();
        }
        public IEnumerable<IProduct> GetAllProducts()
        {
            _logger.LogInformation("Get list Products");
            return _context.Products.ToList();
        }
        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public Cart BuyNow(Cart shoppingCart, string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                throw new Exception("userNotFounded");
            }
            OrderInvoice invoice = new OrderInvoice();
            List<ProductInvoice> list = new List<ProductInvoice>();
            foreach (ProductCart product in shoppingCart.Products)
            {
                ProductInvoice prodInvoice = new ProductInvoice();
                prodInvoice.Product = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                prodInvoice.Qty = product.Qty;
                list.Add(prodInvoice);
            }
            invoice.Products = list;
            invoice.TotalPrice = shoppingCart.Total;
            invoice.Customer = user;

            _context.OrderInvoices.Add(invoice);
            _context.SaveChanges();

            shoppingCart.SuccessPayment = true;

            return shoppingCart;
        }
    }
}
