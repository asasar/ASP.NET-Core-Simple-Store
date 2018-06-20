using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;

namespace SimpleStore.Tests.Models
{
    [TestClass]
    public class SimpleStoreUserTests
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
        public void SimpleStoreUserTest()
        {
            // Arrange
    
            // Act
            SimpleStoreUser simpleStoreUser = this.CreateSimpleStoreUser();

      
            // Assert

        }

        private SimpleStoreUser CreateSimpleStoreUser()
        {
            return new SimpleStoreUser();
        }
    }
}
