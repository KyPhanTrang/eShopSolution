using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest loginRequest);

        Task<ApiResult<PageResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest getUserPagingRequest);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest userUpdateRequest);

        Task<ApiResult<UserViewModel>> GetUserById(Guid id);

        Task<ApiResult<bool>> DeleteById(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest roleAssignRequest);
    }
}