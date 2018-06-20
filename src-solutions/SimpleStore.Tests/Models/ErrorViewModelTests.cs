using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class ErrorViewModelTests
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
        public void ErrorViewModelTest()
        {
            // Arrange
            int random = new Random().Next(111);
            int random2 = new Random().Next(111);
            int random3 = new Random().Next(111);

            // Act
            ErrorViewModel viewModel = this.CreateViewModel();
            viewModel.RequestId = random.ToString();

            // Assert
            Assert.IsTrue(viewModel.RequestId == random.ToString());
            Assert.IsTrue(viewModel.ShowRequestId);
        }

        private ErrorViewModel CreateViewModel()
        {
            return new ErrorViewModel();
        }
    }
}
