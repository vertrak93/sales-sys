using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;
using Sales.Utils.UtilsDto;

namespace Sales.API.Controllers.ProductControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/subcategory")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private SubCategoryService _subCategoryService;
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoryController(IUnitOfWork unitOfWork, SubCategoryService subCategoryService)
        {
            _unitOfWork = unitOfWork;
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var (data, total) = await _subCategoryService.Get(paginationParams);
                return Ok(new ApiResponseDto { Data = data, Total = total, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(SubCategoryDto newObj)
        {
            try
            {
                await _subCategoryService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(SubCategoryDto pathObj)
        {
            try
            {
                await _subCategoryService.Update(pathObj);
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
                await _subCategoryService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
