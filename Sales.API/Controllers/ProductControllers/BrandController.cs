using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UtilsDto;
using Sales.Models;
using Sales.Utils.Enums;
using System.Security.Claims;

namespace Sales.API.Controllers.ProductControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/product/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettingsDto> _appSettings;
        private readonly IMapper _mapper;

        public BrandController(IOptions<AppSettingsDto> appSettings, IUnitOfWork unitOfWork, IMapper mapper, BrandService brandService)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _mapper = mapper;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var brands = await _brandService.Get();
                return Ok(new ApiResponseDto { Data = brands });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(BrandDto brand)
        {
            try
            {
                _unitOfWork.UserName = User.FindFirstValue(ClaimTypes.Name);
                await _brandService.Add(brand);

                return Ok(new ApiResponseDto { });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> PutAsync(BrandDto brand)
        {
            try
            {
                _unitOfWork.UserName = User.FindFirstValue(ClaimTypes.Name);
                await _brandService.Update(brand);
                return Ok(new ApiResponseDto { });
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
                _unitOfWork.UserName = User.FindFirstValue(ClaimTypes.Name);
                await _brandService.Delete(id);
                return Ok(new ApiResponseDto { });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
