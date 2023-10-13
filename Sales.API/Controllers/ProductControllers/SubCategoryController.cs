using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.BLL.Services;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Utils.Constants;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var brands = await _subCategoryService.Get();
                return Ok(new ApiResponseDto { Data = brands, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(SubCategoryDto newObj)
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
        public async Task<IActionResult> Patch(SubCategoryDto pathObj)
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
        public async Task<IActionResult> Delete(int id)
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
