using eShopSolution.Application.System.Languages;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var laguages = await _languageService.GetAll();
            if (!laguages.IsSuccess || laguages.ResultObj == null)
            {
                BadRequest(laguages.ResultObj);
            }
            return Ok(laguages);
        }
    }
}