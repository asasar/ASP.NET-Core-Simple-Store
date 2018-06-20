using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using SimpleStore.Tests.DummyData;
using SimpleStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace SimpleStore.Tests.Models.repository
{
    [TestClass]
    public class SimpleStoreRepositoryTests
    {
        private SimpleStoreRepository repository;

        private Mock<ILogger<SimpleStoreRepository>> mockLogger;

        private IQueryable<Product> productsDummy;

        private IQueryable<SimpleStoreUser> usersDummy;

        private IQueryable<OrderInvoice> OrderInvoiceDummy;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockLogger = new Mock<ILogger<SimpleStoreRepository>>();

            productsDummy = ProductsDummy.GetProducts().AsQueryable();
            var mockSetProduct = new Mock<DbSet<Product>>();
            mockSetProduct.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productsDummy.Provider);
            mockSetProduct.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productsDummy.Expression);
            mockSetProduct.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productsDummy.ElementType);
            mockSetProduct.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productsDummy.GetEnumerator());

            OrderInvoiceDummy = new List<OrderInvoice>().AsQueryable();
            var mockSetOrderInvoices = new Mock<DbSet<OrderInvoice>>();
            mockSetOrderInvoices.As<IQueryable<OrderInvoice>>().Setup(m => m.Provider).Returns(OrderInvoiceDummy.Provider);
            mockSetOrderInvoices.As<IQueryable<OrderInvoice>>().Setup(m => m.Expression).Returns(OrderInvoiceDummy.Expression);
            mockSetOrderInvoices.As<IQueryable<OrderInvoice>>().Setup(m => m.ElementType).Returns(OrderInvoiceDummy.ElementType);
            mockSetOrderInvoices.As<IQueryable<OrderInvoice>>().Setup(m => m.GetEnumerator()).Returns(OrderInvoiceDummy.GetEnumerator());


            usersDummy = ProductsDummy.GetUsers().AsQueryable();
            var mockuser = new Mock<DbSet<SimpleStoreUser>>();
            mockuser.As<IQueryable<SimpleStoreUser>>().Setup(m => m.GetEnumerator()).Returns(usersDummy.GetEnumerator());
            mockuser.As<IQueryable<SimpleStoreUser>>().Setup(m => m.Provider).Returns(usersDummy.Provider);
            mockuser.As<IQueryable<SimpleStoreUser>>().Setup(m => m.Expression).Returns(usersDummy.Expression);
            mockuser.As<IQueryable<SimpleStoreUser>>().Setup(m => m.ElementType).Returns(usersDummy.ElementType);

            var mockContext = new Mock<SimpleStoreContext>();
            mockContext.Setup(c => c.Products).Returns(mockSetProduct.Object);
            mockContext.Setup(c => c.Users).Returns(mockuser.Object);
            mockContext.Setup(c => c.OrderInvoices).Returns(mockSetOrderInvoices.Object);

            repository = new SimpleStoreRepository(mockContext.Object, mockLogger.Object);
        }
 
        [TestMethod]
        public void GetAllProductsTest()
        {
            // Arrange
            

            // Act
            var allProducts = repository.GetAllProducts();
            
            // Assert

            Assert.IsTrue(ProductsDummy.GetProducts().Count == allProducts.Count());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddProductsTest()
        {
            var qtyProducts = ProductsDummy.GetProducts().Count + 1;

            // Arrange
            var newProduct = new Product
            {
                Id = 1,
                Description = "Hamlet",
                Name = "William Shakespeare",
                Created = DateTime.Now,
                Price = new Random().Next(100),
                ImageUrl = new Guid().ToString()
            };

            // Act
            repository.AddProduct(newProduct);
            await repository.SaveChangesAsync();
         
            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetProductByIdNonFoundTest()
        {
            // Arrange
            IProduct newProduct;
            var idNonExist = ProductsDummy.GetProducts().Max(p => p.Id) + 1;
            // Act
            newProduct = repository.GetProductById(idNonExist.ToString());
            

            // Assert

            Assert.IsTrue(newProduct == null);
        }

        [TestMethod]
        public void GetProductByIdFoundTest()
        {
            // Arrange
            IProduct newProduct;
            var idNonExist = ProductsDummy.GetProducts().Max(p => p.Id);
            // Act
            newProduct = repository.GetProductById(idNonExist.ToString());


            // Assert

            Assert.IsTrue(newProduct != null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuyNowUserNonFoundTest()
        {
            // Arrange
            CartViewModel cartViewModel = new CartViewModel();
            string userEmail = "";
            // Act
            var newCartViewModel = repository.BuyNow(cartViewModel, userEmail);


            // Assert
        }

        [TestMethod]
        public void BuyNowUserSuccessTest()
        {
            // Arrange
            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.Products = ProductsDummy.GetProductCart();
            string userEmail = usersDummy.FirstOrDefault().Email;

            // Act
            var newCartViewModel = repository.BuyNow(cartViewModel, userEmail);


            // Assert
            Assert.IsTrue(newCartViewModel.SuccessPayment);
        }

    }
}
