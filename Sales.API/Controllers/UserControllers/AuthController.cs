using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UtilsDto;
using Sales.Utils;
using Sales.Utils.Enums;
using System.Runtime;
using System.Security.Claims;

namespace Sales.API.Controllers.UserControllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettingsDto> _appSettings;
        private readonly IMapper _mapper;

        public AuthController(IOptions<AppSettingsDto> appSettings, IUnitOfWork unitOfWork, IMapper mapper, AuthService authService)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(AuthenticateDto authenticate)
        {
            try
            {
                var data = await _authService.Authenticate(authenticate, _appSettings.Value.KeyJwt, _appSettings.Value.ExpirationTimeJwt, _appSettings.Value.ExpirationRefreshToken);

                return Ok(new ApiResponseDto
                {
                    Code = (int)ApiResponseCodeEnum.OK,
                    Message = "OK",
                    Data= data
                });  

            }catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<ApiResponseDto>> RefreshToken(TokenDto tokens)
        {
            try
            {
                var data = await _authService.RefreshToken(tokens, _appSettings.Value.KeyJwt, _appSettings.Value.ExpirationTimeJwt, _appSettings.Value.ExpirationRefreshToken);

                return Ok(new ApiResponseDto
                {
                    Code = (int)ApiResponseCodeEnum.OK,
                    Message = "OK",
                    Data = data,
                });
                
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

    }
}
