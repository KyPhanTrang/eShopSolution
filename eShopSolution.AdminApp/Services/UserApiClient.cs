using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor) { }

        public async Task<ApiResult<string>> Authenticate(LoginRequest loginRequest)
        {
            return await PostAsync<LoginRequest, string>(loginRequest, "/api/users/authenticate");
        }

        public async Task<ApiResult<bool>> DeleteById(Guid id)
        {
            return await DeleteAsync<bool>($"/api/users/{id}");
        }

        public async Task<ApiResult<UserViewModel>> GetUserById(Guid id)
        {
            return await GetAsync<UserViewModel>($"/api/users/{id}");
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            return await GetAsync<PageResult<UserViewModel>>($"/api/users/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest)
        {
            return await PostAsync<RegisterRequest, bool>(registerRequest, "/api/users/register");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest roleAssignRequest)
        {
            return await PutAsync<RoleAssignRequest, bool>(roleAssignRequest, $"/api/users/{id}/roles");
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest userUpdateRequest)
        {
            return await PutAsync<UserUpdateRequest, bool>(userUpdateRequest, $"/api/users/{id}");
        }
    }
}