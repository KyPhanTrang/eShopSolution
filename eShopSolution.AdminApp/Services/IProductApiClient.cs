using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<ApiResult<PageResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest request);

        Task<bool> Create(ProductCreateRequest request);

        Task<ApiResult<ProductViewModel>> GetProductById(int id, string languageId);

        Task<ApiResult<bool>> CategoryAssign(CategoryAssignRequest request);
    }
}