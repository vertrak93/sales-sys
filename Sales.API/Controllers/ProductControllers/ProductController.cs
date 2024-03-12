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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;
        private VendorProductService _vendorProductService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ProductService productService, VendorProductService vendorProductService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
            _vendorProductService = vendorProductService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> Get(PaginationParams paginationParams)
        {
            try
            {
                var (data, total) = await _productService.Get(paginationParams);
                return Ok(new ApiResponseDto { Data = data, Total = total, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto>> Get(int id)
        {
            try
            {
                var obj = await _productService.GetById(id);
                return Ok(new ApiResponseDto { Data = obj, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> Post(ProductDto newObj)
        {
            try
            {
                await _productService.Add(newObj);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponseDto>> Patch(ProductDto patchObj)
        {
            try
            {
                await _productService.Update(patchObj);
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
                await _productService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpGet("{id}/vendor")]
        public async Task<ActionResult<ApiResponseDto>> GetVendorProdct(int id)
        {
            try
            {
                var obj = await _vendorProductService.GetByProduct(id);
                return Ok(new ApiResponseDto { Data = obj, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
