using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.BLL.Services.ProductServices;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;

namespace Sales.API.Controllers.ProductControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/unitofmeasure")]
    [ApiController]
    public class UnitOfMeasureController : ControllerBase
    {
        private UnitOfMeasureService _unitOfMeasureService;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfMeasureController(IUnitOfWork unitOfWork, UnitOfMeasureService unitOfMeasureService)
        {
            _unitOfWork = unitOfWork;
            _unitOfMeasureService = unitOfMeasureService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get()
        {
            try
            {
                var brands = await _unitOfMeasureService.Get();
                return Ok(new ApiResponseDto { Data = brands, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(UnitOfMeasureDto newObj)
        {
            try
            {
                await _unitOfMeasureService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(UnitOfMeasureDto pathObj)
        {
            try
            {
                await _unitOfMeasureService.Update(pathObj);
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
                await _unitOfMeasureService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
