using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Public;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ApiResult<ProductViewModel>> GetById(int productId, string languageId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<int> UpdateStock(int productId, int addedQuantity);

        Task AddViewCount(int productId);

        Task<ApiResult<PageResult<ProductViewModel>>> GetAllPaging(GetManageProductPagingRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<int> DeleteImage(int imageId);

        Task<List<ProductImageViewModel>> GetImagesByProductId(int productId);

        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
    }
}