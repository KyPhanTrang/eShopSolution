using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManage;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManage = roleManager;
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var roles = await _roleManage.Roles
                .Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();
            return new ApiSuccessResult<List<RoleViewModel>>(roles);
        }
    }
}