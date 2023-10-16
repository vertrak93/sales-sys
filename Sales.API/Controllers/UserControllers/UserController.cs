using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;

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

        [HttpPost]
        public async Task<IActionResult> Post(UserDto newObj)
        {
            try
            {
                await _userService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UserDto obj)
        {
            try
            {
                await _userService.Update(obj);
                return Ok(new ApiResponseDto { Message = Messages.PatchedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(UserDto obj)
        {
            try
            {
                await _userService.ChangePassword(obj);
                return Ok(new ApiResponseDto { Message = Messages.PasswordChanged });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }
    }
}
