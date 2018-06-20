
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleStore.Models;
using SimpleStore.Services;
using SimpleStore.Utils;
using SimpleStore.ViewModels;
using System;

namespace SimpleStore.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailservice;
        private IConfiguration _config;
        private ISimpleStoreRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailservice, IConfiguration configuration, ISimpleStoreRepository repository, ILogger<AppController> logger)
        {
            _config = configuration;
            _mailservice = mailservice;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var products = _repository.GetAllProducts();
                ViewBag.Login = new LoginViewModel();

                GetUserCart();

                return View(products);
            }
            catch (Exception er)
            {
                _logger.LogError($"Error:{er.Message}");
                return View();
            }
        }

        private void GetUserCart()
        {

            CartViewModel shoppingCart = new CartViewModel(GetShoppingCart());

            ViewBag.CartUser = shoppingCart;
        }

        public IActionResult InfoProduct(string id)
        {
            var productInformation = _repository.GetProductById(id);

            return View(productInformation);
        }

        public IActionResult RemoveItem(string id)
        {
            var productInformation = _repository.GetProductById(id);
            RemoveProductToUserCart(productInformation);
            return RedirectToAction("Cart");
        }


        public IActionResult AddToCart(string id)
        {
            var productInformation = _repository.GetProductById(id);
            AddProductToUserCart(productInformation);
            return RedirectToAction("Cart");
        }

        private void AddProductToUserCart(Product productInformation)
        {
            CartViewModel shoppingCart = new CartViewModel(GetShoppingCart());

            if (shoppingCart == null)
                shoppingCart = new CartViewModel(productInformation);
            else
                shoppingCart.AddProduct(productInformation);

            try
            {
                HttpContext.Session.SetObjectAsJson("CartViewModel", shoppingCart);
            }
            catch (Exception)
            {

            }
            ViewBag.CartUser = shoppingCart;
        }


        private void RemoveProductToUserCart(Product productInformation)
        {
            CartViewModel cartUser = new CartViewModel(GetShoppingCart());

            if (cartUser == null)
                cartUser = new CartViewModel(productInformation);
            else
                cartUser.RemoveProduct(productInformation);

            HttpContext.Session.SetObjectAsJson("CartViewModel", cartUser);
            ViewBag.CartUser = cartUser;
        }

        private Cart GetShoppingCart()
        {
            Cart cartUser = null;
            try
            {
                cartUser = HttpContext.Session.GetObjectFromJson<Cart>("CartViewModel");
            }
            catch (Exception)
            {
            }

            return cartUser;
        }

        public IActionResult Cart()
        {
            GetUserCart();
            return View();
        }



        [Authorize(Roles = "Customer")]
        public IActionResult BuyNow()
        {

            CartViewModel shoppingCartUser = new CartViewModel(GetShoppingCart());
            try
            {
                if (shoppingCartUser != null)
                {
                    string userEmail = User.Identity.Name;
                    var resultBueNow = _repository.BuyNow(shoppingCartUser, userEmail);
                    if (resultBueNow.SuccessPayment)
                    {
                        HttpContext.Session.SetObjectAsJson("CartViewModel", null);
                        shoppingCartUser.SuccessPayment = resultBueNow.SuccessPayment;
                    }
                }

            }
            catch (Exception)
            {
            }

            return View(shoppingCartUser);
        }

        public IActionResult Contact()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (contact.Email.Contains("me@"))
            {
                ModelState.AddModelError("Email", "Don't allow email from me@");
            }

            if (ModelState.IsValid)
            {
                _mailservice.SendMail(_config["EmailSettings:emailTo"], contact.Email, _config["EmailSettings:subject"], contact.Message);

                ViewBag.UserMessage = contact.Message;
                ModelState.Clear();
            }
            return View();
        }
    }
}
