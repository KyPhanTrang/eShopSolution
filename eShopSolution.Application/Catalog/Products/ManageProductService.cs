using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                ProductId = productId,
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                SortOrder = request.SortOrder
            };
            if (request.ImageFile != null)
            {
                productImage.FileSize = request.ImageFile.Length;
                productImage.ImagePath = await SaveFile(request.ImageFile);
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            if (request == null)
                return -1;

            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                    {
                        new ProductTranslation()
                        {
                            Name = request.Name,
                            Description = request.Description,
                            Details = request.Details,
                            SeoDescription = request.SeoDescription,
                            SeoAlias = request.SeoAlias,
                            SeoTitle = request.SeoTitle,
                            LanguageId = request.LanguageId
                        }
                    }
            };
            // Save image
            if (request.ThumbnailImage != null)
            {
                var image = new ProductImage()
                {
                    Caption = "Thumbnail",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbnailImage.Length,
                    ImagePath = await SaveFile(request.ThumbnailImage),
                    IsDefault = true
                };
                product.ProductImages = new List<ProductImage> { image };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new EShopException($"Not found image with {imageId}");
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            _context.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<PageResult<ProductViewModel>>> GetAllPaging(GetManageProductPagingRequest request)
        {
            // 1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        //join pic in _context.ProductInCategories on pt.ProductId equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.Id
                        //where pt.LanguageId == request.LanguageId
                        select new { p, pt };
            // Filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            //if (request.CategoryIds != null && request.CategoryIds.Count > 0)
            //    query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));

            // 3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    ProductId = x.p.Id,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            if (data.Count == 0)
            {
                return new ApiErrorResult<PageResult<ProductViewModel>>("Not exists product");
            }

            // 4. Select and project
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return new ApiSuccessResult<PageResult<ProductViewModel>>(pageResult);
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) return null;

            var productTranslation = product.ProductTranslations.FirstOrDefault(x => x.ProductId == productId
            && x.LanguageId == languageId);

            return new ProductViewModel()
            {
                ProductId = productId,
                Description = productTranslation?.Description,
                Details = productTranslation?.Details,
                LanguageId = productTranslation?.LanguageId,
                Name = productTranslation?.Name,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                Stock = product.Stock,
                DateCreated = product.DateCreated,
                SeoAlias = productTranslation?.SeoAlias,
                SeoDescription = productTranslation?.SeoDescription,
                SeoTitle = productTranslation?.SeoTitle,
                ViewCount = product.ViewCount
            };
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var productImageViewModel = await _context.ProductImages
                .AsNoTracking()
                .Where(x => x.Id == imageId)
                .Select(x => new ProductImageViewModel()
                {
                    Id = x.Id,
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    ImagePath = x.ImagePath,
                    ProductId = x.ProductId,
                    IsDefault = x.IsDefault,
                    SortOrder = x.SortOrder
                }).FirstOrDefaultAsync();

            if (productImageViewModel == null) throw new EShopException($"Cannot find an image with id: {imageId}");
            return productImageViewModel;
        }

        public async Task<List<ProductImageViewModel>> GetImagesByProductId(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    ProductId = i.ProductId,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder,
                    IsDefault = i.IsDefault
                }).ToListAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null) throw new EShopException($"Cannot find a product with id: {request.Id}");

            productTranslations.Name = request.Name;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.SeoAlias = request.SeoAlias;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);

            if (productImage == null) throw new EShopException($"Cannot find an image with id: {imageId}");
            else await _storageService.DeleteFileAsync(productImage.ImagePath); // Delete before update new image

            if (request.ImageFile != null)
            {
                productImage.Caption = request.Caption;
                productImage.IsDefault = request.IsDefault;
                productImage.SortOrder = request.SortOrder;
                productImage.FileSize = request.ImageFile.Length;
                productImage.ImagePath = await SaveFile(request.ImageFile);
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product with id {productId}");

            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product with id:{productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}