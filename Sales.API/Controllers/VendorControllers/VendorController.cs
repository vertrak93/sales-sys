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
    [Route("api/vendor")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private VendorService _vendorService;
        private VendorProductService _vendorProductService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorController(IUnitOfWork unitOfWork, IMapper mapper, VendorService vendorService, VendorProductService vendorProductService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorService = vendorService;
            _vendorProductService = vendorProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var obj = await _vendorService.Get();
                return Ok(new ApiResponseDto { Data = obj, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _vendorService.GetById(id);
                return Ok(new ApiResponseDto { Data = obj, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendorDto newObj)
        {
            try
            {
                await _vendorService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(VendorDto patchObj)
        {
            try
            {
                await _vendorService.Update(patchObj);
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
                await _vendorService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpGet("{id}/product")]
        public async Task<IActionResult> GetVendorProdct(int id)
        {
            try
            {
                var obj = await _vendorProductService.GetByVendor(id);
                return Ok(new ApiResponseDto { Data = obj, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
