using eShopSolution.Application.System;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous] // Don't need login, u still cant call that
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultToken = await _userService.Authenticate(loginRequest);
            if (string.IsNullOrEmpty(resultToken))
                return BadRequest("Username or password is incorrect.");

            return Ok(resultToken);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.Register(registerRequest);

            if (result == false) return BadRequest("Register is unsuccessful");

            return Ok();
        }

        //https://localhost/api/users/paging?pagIndex=1&pageSize=2&keyword=...
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }
    }
}