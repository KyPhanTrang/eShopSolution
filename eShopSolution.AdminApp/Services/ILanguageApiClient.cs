using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        public Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}