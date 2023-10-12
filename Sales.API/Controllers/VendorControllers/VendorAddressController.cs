using AutoMapper;
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
    [Route("api/vendor/address")]
    [ApiController]
    public class VendorAddressController : ControllerBase
    {
        private VendorAddressService _vendorAddressService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorAddressController(IUnitOfWork unitOfWork, IMapper mapper, VendorAddressService vendorAddressService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorAddressService = vendorAddressService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendorAddressDto newObj)
        {
            try
            {
                await _vendorAddressService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(VendorAddressDto patchObj)
        {
            try
            {
                await _vendorAddressService.Update(patchObj);
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
                await _vendorAddressService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
