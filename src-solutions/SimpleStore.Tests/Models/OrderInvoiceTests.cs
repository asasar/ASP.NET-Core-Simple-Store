using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;

namespace SimpleStore.Tests.Models
{
	[TestClass]
public class OrderInvoiceTests
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
    public void OrderInvoiceTest()
    {
        // Arrange


        // Act
        OrderInvoice orderInvoice = this.CreateOrderInvoice();


        // Assert

    }

    private OrderInvoice CreateOrderInvoice()
    {
        return new OrderInvoice();
    }
}
}
