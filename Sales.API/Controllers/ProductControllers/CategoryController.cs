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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, CategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var (data, total) = await _categoryService.Get(paginationParams);
                return Ok(new ApiResponseDto { Data = data, Total = total, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(CategoryDto category)
        {
            try
            {
                await _categoryService.Add(category);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(CategoryDto category)
        {
            try
            {
                await _categoryService.Update(category);
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
                await _categoryService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

    }
}
