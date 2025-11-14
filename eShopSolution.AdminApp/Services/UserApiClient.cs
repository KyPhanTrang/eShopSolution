using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest loginRequest)
        {
            var json = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            var response = await client.PostAsync("/api/users/authenticate", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<bool>> DeleteById(Guid id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"/api/users/{id}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<ApiResult<UserViewModel>> GetUserById(Guid id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/users/{id}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/users/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<ApiSuccessResult<PageResult<UserViewModel>>>(body);
            return users;
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest)
        {
            var json = JsonConvert.SerializeObject(registerRequest); // convert type to json
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); //Wrap JSON in outgoing content (HTTP Body)

            var client = _httpClientFactory.CreateClient(); // Create httpclient from factory
            client.BaseAddress = new Uri(_config["BaseAddress"]);

            // Send post request to API
            var response = await client.PostAsync("/api/users/register", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest roleAssignRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var json = JsonConvert.SerializeObject(roleAssignRequest);
            var httpContent = new StringContent(json, UTF8Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest userUpdateRequest)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var json = JsonConvert.SerializeObject(userUpdateRequest); // convert type to json
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); //Wrap JSON in outgoing content (HTTP Body)

            var client = _httpClientFactory.CreateClient(); // Create httpclient from factory
            client.BaseAddress = new Uri(_config["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Send post request to API
            var response = await client.PutAsync($"/api/users/{id}", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}