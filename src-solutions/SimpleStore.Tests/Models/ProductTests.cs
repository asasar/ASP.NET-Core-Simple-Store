using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ProductConstructorTest()
        {
            // Arrange


            // Act
            IProduct product = this.CreateProduct();


            // Assert
            Assert.IsTrue(product.Id == 0);
            Assert.IsNull(product.Name);
        }

        [TestMethod]
        public void ProductGetSetTest()
        {
            // Arrange

            string name = Guid.NewGuid().ToString();
            int random = new Random().Next(111);
            // Act
            IProduct product = this.CreateProduct();
            product.Id = random;
            product.Name = name;

            // Assert
            Assert.IsTrue(product.Id == random);
            Assert.IsTrue(product.Name == name);
        }


        private IProduct CreateProduct()
        {
            return new Product();
        }
    }
}
