using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _configuration;
        private readonly EShopDbContext _dbContext;

        public LanguageService(IConfiguration configuration, EShopDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            var languagesViewModel = await _dbContext.Languages.Select(x => new LanguageViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            if (languagesViewModel == null)
            {
                return new ApiErrorResult<List<LanguageViewModel>>("Language is empty");
            }

            return new ApiSuccessResult<List<LanguageViewModel>>(languagesViewModel);
        }
    }
}