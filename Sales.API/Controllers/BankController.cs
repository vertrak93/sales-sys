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
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private BankService _bankService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankController(IUnitOfWork unitOfWork, IMapper mapper, BankService bankService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var objs = await _bankService.Get();
                return Ok(new ApiResponseDto { Data = objs, Message = Messages.GetedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(BankDto bank)
        {
            try
            {
                await _bankService.Add(bank);

                return Ok(new ApiResponseDto { Message = Messages.PostedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(BankDto bank)
        {
            try
            {
                await _bankService.Update(bank);
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
                await _bankService.Delete(id);
                return Ok(new ApiResponseDto { Message = Messages.DeletedData });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto.ErrorHandler(ex));
            }
        }
    }
}
