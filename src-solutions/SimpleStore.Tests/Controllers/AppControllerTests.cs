using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleStore.Controllers.Web;
using SimpleStore.Models;
using SimpleStore.Services;
using SimpleStore.Tests.DummyData;
using SimpleStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Tests.Controllers
{
    [TestClass]
    public class AppControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IMailService> mockMailService;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<ISimpleStoreRepository> mockSimpleStoreRepository;
        private Mock<ILogger<AppController>> mockLogger;

      
        [TestInitialize]
        public void TestInitialize()
        {

            //CartViewModel shoppingCart, string userEmail
            this.mockRepository = new MockRepository(MockBehavior.Loose);

            this.mockMailService = this.mockRepository.Create<IMailService>();
            this.mockConfiguration = this.mockRepository.Create<IConfiguration>();
            this.mockSimpleStoreRepository = this.mockRepository.Create<ISimpleStoreRepository>();

            this.mockSimpleStoreRepository.Setup(r => r.GetAllProducts()).Returns(ProductsDummy.GetProducts());
            this.mockSimpleStoreRepository.Setup(r => r.GetProductById(It.IsAny<string>())).Returns(ProductsDummy.GetProducts().FirstOrDefault());

            this.mockLogger = this.mockRepository.Create<ILogger<AppController>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void HomeControllerTest()
        {
            // Arrange

            AppController appController = this.CreateAppController();
            appController.ControllerContext = new ControllerContext();

            appController.ControllerContext.HttpContext = new DefaultHttpContext();
            appController.ControllerContext.HttpContext.Session = new MockHttpSession();

            // Act

            ViewResult infoProduct = appController.Index() as ViewResult;

            // Assert

            var result = (infoProduct.Model as List<Product>)[0];
            Assert.IsTrue(result.Id == ProductsDummy.GetProducts()[0].Id);
        }


        [TestMethod]
        public void BuyProductTest()
        {
            // Arrange


            AppController appController = this.CreateAppController();
            appController.ControllerContext = new ControllerContext();

            appController.ControllerContext.HttpContext = new DefaultHttpContext();
            appController.ControllerContext.HttpContext.Session = new MockHttpSession();

            var oneProduct = ProductsDummy.GetProducts().FirstOrDefault();

            // Act

            appController.Index();
            appController.InfoProduct(oneProduct.Id.ToString());
            appController.AddToCart(oneProduct.Id.ToString());

            this.mockSimpleStoreRepository
           .Setup(r => r.BuyNow(It.IsAny<Cart>(), It.IsAny<string>()))
                    .Returns(new Cart() { SuccessPayment = true });

            var viewActionBuy = appController.BuyNow() as ViewResult;

            // Assert
            var model = viewActionBuy.Model as Cart;

            Assert.IsTrue(model.SuccessPayment);
        }


        private AppController CreateAppController()
        {
            return new AppController(
                this.mockMailService.Object,
                this.mockConfiguration.Object,
                this.mockSimpleStoreRepository.Object,
                this.mockLogger.Object);
        }
    }
}
