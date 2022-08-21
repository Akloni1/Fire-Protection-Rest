using Fire.Services.ProductServices;
using Fire.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductViewModels>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _productServices.GetAllProducts();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductViewModels))]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await _productServices.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Authorize]
        [Route("cost")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ProductViewModels))]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetCostById(int id, decimal km)
        {
            var cost = await _productServices.GetCostById(id, km);
            return Ok(cost);
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> PostProduct(InputProductViewModels inputModel)
        {

            var product = await _productServices.AddProduct(inputModel);

            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, EditProductViewModels editModel)
        {

            var product = await _productServices.UpdateProduct(id, editModel);
            if (product == null) return BadRequest();
            return Ok(product);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProductViewModels>> DeleteProduct(int id)
        {

            var product = await _productServices.DeleteProduct(id);
            if (product == null) return NotFound();
            return Ok(product);
        }


    }
}
