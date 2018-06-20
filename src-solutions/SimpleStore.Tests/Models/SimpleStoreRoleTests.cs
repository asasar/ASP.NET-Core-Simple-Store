using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;

namespace SimpleStore.Tests.Models
{
	[TestClass]
public class SimpleStoreRoleTests
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
    public void SimpleStoreRoleTest()
    {
        // Arrange


        // Act
        SimpleStoreRole simpleStoreRole = this.CreateSimpleStoreRole();


        // Assert

    }

    private SimpleStoreRole CreateSimpleStoreRole()
    {
        return new SimpleStoreRole();
    }
}
}
