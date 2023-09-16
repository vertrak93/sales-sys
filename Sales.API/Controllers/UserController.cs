using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private UserService _userService;
        public UserController(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
            _userService = new UserService(_unitOfWork);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _userService.GetUsers();

            return Ok(data);
        }
    }
}
