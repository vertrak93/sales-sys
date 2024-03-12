using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils.Constants;
using Sales.Utils.UtilsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class PresentationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PresentationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(PresentationDto presentation)
        {
            var objPresentation = _mapper.Map<Presentation>(presentation);
            await _unitOfWork.Presentations.Add(objPresentation);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<PresentationDto>,int)> Get(PaginationParams paginationParams)
        {
            var presentation = _unitOfWork.Presentations.Get()
                .Where(o => o.Active == true)
                .Select(o=> new PresentationDto
                {
                    PresentationId = o.PresentationId,
                    PresentationName = o.PresentationName
                });

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                presentation = presentation.Where(item =>
                    item.PresentationName.ToUpper().Contains(paginationParams.Filter.ToUpper()));
            }

            var total = (await presentation.CountAsync());
            var result = await presentation.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);

        }

        public async Task<bool> Update(PresentationDto presentation)
        {
            var dbObj = await _unitOfWork.Presentations.Get(presentation.PresentationId);
            var objPresentation = _mapper.Map(presentation, dbObj);
            var obj = _unitOfWork.Presentations.Update(objPresentation);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int PresentationId)
        {
            await ValidateDeletePresentation(PresentationId);
            var obj = await _unitOfWork.Presentations.Delete(PresentationId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeletePresentation(int PresentationId)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.PresentationId == PresentationId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.PresentationUsed);
            }
        }
    }
}
