using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, configuration, httpContextAccessor)
        { }

        public async Task<ApiResult<List<CategoryViewModel>>> GetALl(string languageId)
        {
            return await GetAsync<List<CategoryViewModel>>("/api/categories?languageId=" + languageId);
        }
    }
}