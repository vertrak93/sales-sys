using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;

namespace Sales.API.Controllers.UserControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private UserService _userService;
        public UserController(IUnitOfWork unitOfWork, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _userService.Get();
            return Ok(data);
        }
    }
}
