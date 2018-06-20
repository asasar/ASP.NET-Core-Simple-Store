using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Models;
using System;

namespace SimpleStore.Tests.Models
{
	[TestClass]
public class HistoryStatusTests
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
    public void HistoryStatusTest()
    {
            // Arrange
            int random = new Random().Next(111);
            int random2 = new Random().Next(111);
            DateTime now = DateTime.Now;
            // Act
            HistoryStatus historyStatus = this.CreateHistoryStatus();
            StatusInformation StatusOrder = new StatusInformation();
            StatusOrder.Id = random;
            historyStatus.Id = random;
            historyStatus.Created = now;
            historyStatus.StatusOrder = StatusOrder;
            // Assert
            Assert.IsTrue(historyStatus.Id == random);
            Assert.IsTrue(historyStatus.Created == now);
            Assert.IsTrue(historyStatus.StatusOrder.Id == StatusOrder.Id);
        }

    private HistoryStatus CreateHistoryStatus()
    {
        return new HistoryStatus();
    }
}
}
