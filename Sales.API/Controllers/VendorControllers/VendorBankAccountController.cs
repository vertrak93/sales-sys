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
    [Route("api/vendor/bank-account")]
    [ApiController]
    public class VendorBankAccountController : ControllerBase
    {
        private VendorBankAccountService _vendorBankAccountService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorBankAccountController(IUnitOfWork unitOfWork, IMapper mapper, VendorBankAccountService vendorBankAccountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorBankAccountService = vendorBankAccountService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(VendorBankAccountDto newObj)
        {
            try
            {
                await _vendorBankAccountService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(VendorBankAccountDto patchObj)
        {
            try
            {
                await _vendorBankAccountService.Update(patchObj);
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
                await _vendorBankAccountService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
