using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.BLL.Services.UserServices;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UserDtos;
using Sales.Utils.Constants;

namespace Sales.API.Controllers.UserControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/user/role")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private UserRoleService _userRoleService;
        public UserRoleController(IUnitOfWork unitOfWork, UserRoleService userRoleService)
        {
            _unitOfWork = unitOfWork;
            _userRoleService = userRoleService;
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(int userid)
        {
            try
            {
                var data = await _userRoleService.GetByUserIdAsigned(userid);
                return Ok(new ApiResponseDto { Data = data, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserRoleDto newObj)
        {
            try
            {
                await _userRoleService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
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
                await _userRoleService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
