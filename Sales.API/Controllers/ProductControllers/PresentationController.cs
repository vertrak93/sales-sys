using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UtilsDto;
using Sales.Utils.Constants;
using Sales.Utils.UtilsDto;

namespace Sales.API.Controllers.ProductControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/presentation")]
    [ApiController]
    public class PresentationController : ControllerBase
    {
        private PresentationService _presentationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PresentationController(IUnitOfWork unitOfWork, IMapper mapper, PresentationService presentationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _presentationService = presentationService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get(PaginationParams paginationParams)
        {
            try
            {
                var (data, total) = await _presentationService.Get(paginationParams);
                return Ok(new ApiResponseDto { Data = data, Total = total, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(PresentationDto presentation)
        {
            try
            {
                await _presentationService.Add(presentation);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(PresentationDto presentation)
        {
            try
            {
                await _presentationService.Update(presentation);
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
                await _presentationService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
