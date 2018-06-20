using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class SimpleStoreContextTests
    {
        private MockRepository mockRepository;

        private Mock<DbContextOptions<SimpleStoreContext>> mockDbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDbContextOptions = this.mockRepository.Create<DbContextOptions<SimpleStoreContext>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void TestSimpleStoreContext()
        {
            // Arrange


            // Act
            SimpleStoreContext simpleStoreContext = this.CreateSimpleStoreContext();

            // Assert

            Assert.IsTrue(simpleStoreContext.Products != null);
            Assert.IsTrue(simpleStoreContext.OrderInvoices != null);
            Assert.IsTrue(simpleStoreContext.ProductsCategories != null);
            Assert.IsTrue(simpleStoreContext.OrderInvoices != null);
        }

        private SimpleStoreContext CreateSimpleStoreContext()
        {
            return new SimpleStoreContext( new DbContextOptions<SimpleStoreContext>());
        }
    }
}
