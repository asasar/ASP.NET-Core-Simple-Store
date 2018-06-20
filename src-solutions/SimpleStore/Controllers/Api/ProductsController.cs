using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleStore.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    [Route("api/Products")]
    public class ProductsApiController : Controller
    {
        private ILogger<ProductsApiController> _logger;
        private ISimpleStoreRepository _repo;

        public ProductsApiController(ISimpleStoreRepository repo, ILogger<ProductsApiController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Get()
        {
            try
            {
                var products = _repo.GetAllProducts()
                    .Select(p => new ProductViewModel()
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Created = p.Created,
                        EnabledUser = p.EnabledUser,
                        ImageUrl = p.ImageUrl
                    });
                return Ok(products);
            }
            catch (Exception err)
            {
                _logger.LogError($"Failed Get Products. {err}");
                return BadRequest(err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductViewModel product)
        {
            try
            {
                var newProduct = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Created = product.Created,
                    EnabledUser = product.EnabledUser,
                    ImageUrl = product.ImageUrl
                };
                if (ModelState.IsValid)
                {
                    _repo.AddProduct(newProduct);
                    if (await _repo.SaveChangesAsync())
                    {
                        product = new ProductViewModel()
                        {
                            Name = newProduct.Name,
                            Description = newProduct.Description,
                            Price = newProduct.Price,
                            Created = newProduct.Created,
                            EnabledUser = newProduct.EnabledUser,
                            ImageUrl = newProduct.ImageUrl
                        };
                        return Created($"Created Product Name:{product.Name}", product);
                    }
                    else
                    {
                        return BadRequest("Failed to save changes Changes");
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
