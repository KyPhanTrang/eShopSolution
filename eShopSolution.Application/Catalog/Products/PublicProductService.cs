using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Public;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _context;
        public PublicProductService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll(string languageId)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic};

            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                DateCreated = x.p.DateCreated,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Name = x.pt.Name,
                ProductId = x.p.Id,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount
            }).ToListAsync();

            return data;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            // 1. Select and join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };
            // 2. Filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);

            // 3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    DateCreated = x.p.DateCreated,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Name = x.pt.Name,
                    ProductId = x.p.Id,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}



