using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
	[TestClass]
public class ProductCategoryTests
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
    public void ProductCategoryTest()
    {
            // Arrange

            int random = new Random().Next(111);
            int random2 = new Random().Next(111);
            int random3 = new Random().Next(111);
            // Act
            ProductCategory productCategory = this.CreateProductCategory();
            productCategory.Id = random;
            productCategory.IdCategory = random2;
            productCategory.IdProduct = random3;

            // Assert
            Assert.IsTrue(productCategory.Id == random);
            Assert.IsTrue(productCategory.IdCategory == random2);
            Assert.IsTrue(productCategory.IdProduct == random3);
        }

    private ProductCategory CreateProductCategory()
    {
        return new ProductCategory();
    }
}
}
