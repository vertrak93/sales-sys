﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UtilsDto;
using Sales.Utils.Constants;

namespace Sales.API.Controllers.ProductControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper, BrandService brandService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var brands = await _brandService.Get();
                return Ok(new ApiResponseDto { Data = brands, Message = Messages.GetedData });
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
                await _brandService.Add(brand);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(BrandDto brand)
        {
            try
            {
                await _brandService.Update(brand);
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
                await _brandService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
