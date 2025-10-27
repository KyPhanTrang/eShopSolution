using eShopSolution.ViewModels.Common;

namespace eShopSolution.ViewModels.Catalog.Products.Dtos.Public
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
