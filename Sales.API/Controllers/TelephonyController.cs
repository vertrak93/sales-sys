using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;

namespace Sales.API.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/telephony")]
    [ApiController]
    public class TelephonyController : ControllerBase
    {
        private TelephonyService _telephonyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TelephonyController(IUnitOfWork unitOfWork, IMapper mapper, TelephonyService telephonyService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _telephonyService = telephonyService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get()
        {
            try
            {
                var objs = await _telephonyService.Get();
                return Ok(new ApiResponseDto { Data = objs, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(TelephonyDto newObj)
        {
            try
            {
                await _telephonyService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(TelephonyDto obj)
        {
            try
            {
                await _telephonyService.Update(obj);
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
                await _telephonyService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
