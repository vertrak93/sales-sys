using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;
using Sales.Utils.UtilsDto;

namespace Sales.API.Controllers.UserControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var (data, total) = await _userService.Get(paginationParams);
                return Ok(new ApiResponseDto { Data = data, Total= total, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(UserDto newObj)
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
        public async Task<ActionResult<ApiResponseDto>> Patch(UserDto obj)
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
        public async Task<ActionResult<ApiResponseDto>> Delete(int id)
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
        public async Task<ActionResult<ApiResponseDto>> ChangePassword(UserDto obj)
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
