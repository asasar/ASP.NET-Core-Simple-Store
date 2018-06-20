using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
	[TestClass]
public class ProductInvoiceTests
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
    public void ProductInvoiceTest()
    {
            // Arrange
            string name = Guid.NewGuid().ToString();
            int random = new Random().Next(111);
            int random2 = new Random().Next(111);
            // Act

            // Act
            ProductInvoice productInvoice = this.CreateProductInvoice();

            productInvoice.Id = random;
            productInvoice.Product = new Product();
            productInvoice.Product.Id = random;
            productInvoice.Qty = random2;


            // Assert
            Assert.IsTrue(productInvoice.Id == random);
            Assert.IsTrue(productInvoice.Product.Id == random);
            Assert.IsTrue(productInvoice.Qty == random2);

        }

    private ProductInvoice CreateProductInvoice()
    {
        return new ProductInvoice();
    }
}
}
