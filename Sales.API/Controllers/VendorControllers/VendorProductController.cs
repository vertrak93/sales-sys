﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;

namespace Sales.API.Controllers.VendorControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/vendor/product")]
    [ApiController]
    public class VendorProductController : ControllerBase
    {
        private VendorProductService _vendorProductService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorProductController(IUnitOfWork unitOfWork, IMapper mapper, VendorProductService vendorProductService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorProductService = vendorProductService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(VendorProductDto newObj)
        {
            try
            {
                await _vendorProductService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(VendorProductDto patchObj)
        {
            try
            {
                await _vendorProductService.Update(patchObj);
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
                await _vendorProductService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
