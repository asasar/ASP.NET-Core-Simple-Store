using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class StatusInformationTests
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
        public void StatusInformationTest()
        {
            // Arrange

            string test = Guid.NewGuid().ToString();
            int random = new Random().Next(111);

            int random2 = new Random().Next(111);

            // Act
            StatusInformation statusInformation = this.CreateStatusInformation();
            statusInformation.Id = random;
            statusInformation.Description = test;
            statusInformation.Group = random2;

            // Assert

            Assert.IsTrue(statusInformation.Id == random);
            Assert.IsTrue(statusInformation.Description == test);
            Assert.IsTrue(statusInformation.Group == random2);

        }

        private StatusInformation CreateStatusInformation()
        {
            return new StatusInformation();
        }
    }
}
