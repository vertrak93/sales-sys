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
    [Route("api/vendor/phone")]
    [ApiController]
    public class VendorPhoneController : ControllerBase
    {
        private VendorPhoneService _vendorPhoneService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorPhoneController(IUnitOfWork unitOfWork, IMapper mapper, VendorPhoneService vendorPhoneService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorPhoneService = vendorPhoneService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendorPhoneDto newObj)
        {
            try
            {
                await _vendorPhoneService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(VendorPhoneDto patchObj)
        {
            try
            {
                await _vendorPhoneService.Update(patchObj);
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
                await _vendorPhoneService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
