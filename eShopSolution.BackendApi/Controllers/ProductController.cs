using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Public;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        // http://localhost:port/product => url format
        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);
            return Ok(products);
        }

        // http://localhost:port/product => equal up => add http://localhost:port/product/public-paging
        [HttpGet("public-paging/{languageId}")]
        public async Task<IActionResult> Get(
            string languageId, 
            [FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(languageId, request);
            return Ok(product);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            if (productId == -1)
                return BadRequest();

            var productViewModel = await _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            var product = await _manageProductService.GetById(request.Id, request.LanguageId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // In HttpDelete("{x}"), if inside is "x" ==> in Delete(x)
        {
            var affectedResult = await _manageProductService.Delete(id);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessFul = await _manageProductService.UpdatePrice(productId, newPrice);
            if (!isSuccessFul)
                return BadRequest();
            return Ok();
        }
    } 
}
