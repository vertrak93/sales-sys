using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.BLL.Services.UserServices;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UtilsDto;
using Sales.Utils;
using System.Runtime;
using System.Security.Claims;

namespace Sales.API.Controllers.UserControllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettingsDto> _appSettings;
        private readonly IMapper _mapper;

        public AuthController(IOptions<AppSettingsDto> appSettings, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthenticateDto authenticate)
        {
            try
            {
                using (var authService = new AuthService(_unitOfWork, _mapper))
                {
                    _unitOfWork.UserName = authenticate.Username;
                    var data = await authService.Authenticate(authenticate, _appSettings.Value.KeyJwt);

                    return Ok(new ApiResponseDto
                    {
                        Code = 200,
                        Message = "OK",
                        Data= data
                    });
                }   

            }catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }


        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenDto tokens)
        {
            try
            {
                using (var authService = new AuthService(_unitOfWork, _mapper))
                {
                    _unitOfWork.UserName = TokenGenerator.Instance().GetUserFromJwt(tokens.Jwt, _appSettings.Value.KeyJwt);
                    var data = await authService.RefreshToken(tokens, _appSettings.Value.KeyJwt);

                    return Ok(new ApiResponseDto
                    {
                        Code = 200,
                        Message = "OK",
                        Data = data,
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

    }
}
