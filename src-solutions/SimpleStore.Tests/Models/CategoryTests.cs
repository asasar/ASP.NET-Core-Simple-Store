using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class CategoryTests
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
        public void CategoryConstructorTest()
        {
            // Arrange


            // Act
            Category category = this.CreateCategory();


            // Assert
            Assert.IsTrue(category.Id == 0);
            Assert.IsNull(category.Name);
        }

        [TestMethod]
        public void CategoryGetSetTest()
        {
            // Arrange

            string name = Guid.NewGuid().ToString();
            int random = new Random().Next(111);
            // Act
            Category category = this.CreateCategory();
            category.Id = random;
            category.Name = name;

            // Assert
            Assert.IsTrue(category.Id == random);
            Assert.IsTrue(category.Name == name);
        }


        private Category CreateCategory()
        {
            return new Category();
        }
    }
}
