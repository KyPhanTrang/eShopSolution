using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products.Dtos.Manage;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _config;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient,
            IConfiguration config,
            IRoleApiClient roleApiClient,
            ICategoryApiClient categoryApiClient)
        {
            _config = config;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            if (string.IsNullOrEmpty(session)) return RedirectToAction("Login", "User");

            var request = new GetManageProductPagingRequest()
            {
                LanguageId = languageId,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };

            var data = await _productApiClient.GetProductsPaging(request);

            var categories = await _categoryApiClient.GetALl(languageId);
            if (categories.ResultObj != null)
            {
                ViewBag.Categories = categories.ResultObj.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = categoryId.HasValue && categoryId == x.Id
                });
            }

            ViewBag.Message = TempData["Message"];
            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Keyword = keyword;

            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.Create(request);
            if (result)
            {
                TempData["Message"] = "Thêm mới sản phẩm thành công!";
                TempData["IsSuccess"] = true;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Create product is failed");
            return View(request);
        }
    }
}