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
        public async Task<IActionResult> Get()
        {
            try
            {
                var brands = await _presentationService.Get();
                return Ok(new ApiResponseDto { Data = brands, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(PresentationDto presentation)
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
        public async Task<IActionResult> Patch(PresentationDto presentation)
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
        public async Task<IActionResult> Delete(int id)
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
