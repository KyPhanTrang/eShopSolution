using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Public;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProductService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        // http://localhost:port/product?languageId=vi-VN?pageIndex=1&pageSize=10&categoryId=1
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetProductPaging(
            string languageId,
            [FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(languageId, request);
            return Ok(product);
        }

        //http:localhost:port/product/1
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == -1)
                return BadRequest();

            var productViewModel = await _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productViewModel);
        }

        [HttpPut] // Update all use Put
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            var product = await _manageProductService.GetById(request.Id, request.LanguageId);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId) // In HttpDelete("{x}"), if inside is "x" ==> in Delete(x)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]  // Patch ==> update part of product
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessFul = await _manageProductService.UpdatePrice(productId, newPrice);
            if (!isSuccessFul)
                return BadRequest();
            return Ok();
        }

        // Get image by id
        [HttpGet("{productId}/images/{imageId}")] // Nested Routing (parent => child)
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return NotFound("Cannot find image");
            return Ok(image);
        }

        // Add Image
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var productViewModel = await _manageProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productViewModel);
        }

        // Update image
        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manageProductService.UpdateImage(imageId, request);
            if (result == 0) return BadRequest();

            return Ok();
        }

        // Delete image
        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int productId, int imageId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = await _manageProductService.DeleteImage(imageId);
            if (result == 0) return BadRequest();

            return Ok();
        }
    }
}