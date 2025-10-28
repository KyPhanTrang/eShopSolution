using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Public;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<ProductViewModel> GetById(int productId, string languageId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<int> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> AddImages(int productId, List<IFormFile> images);
        Task<int> RemoveImage(int imageId);
        Task<int> DeleteImages(int imageId, string caption);
        Task<ProductImageViewModel> GetListImage(int productId);
    }
}
