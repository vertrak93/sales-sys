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

namespace Sales.BLL.Services.ProductServices
{
    public class UnitOfMeasureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfMeasureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(UnitOfMeasureDto unitOfMeasure)
        {
            var objUnitOfMeasure = _mapper.Map<UnitOfMeasure>(unitOfMeasure);
            await _unitOfWork.UnitOfMeasures.Add(objUnitOfMeasure);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<UnitOfMeasureDto>,int)> Get(PaginationParams paginationParams)
        {
            var unitOfMeasure = _unitOfWork.UnitOfMeasures.Get()
                .Where(o => o.Active == true)
                .Select(o => new UnitOfMeasureDto
                {
                    UnitOfMeasureId = o.UnitOfMeasureId,
                    UnitOfMeasureName = o.UnitOfMeasureName,
                    Abbreviation = o.Abbreviation,
                });

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                unitOfMeasure = unitOfMeasure.Where(item =>
                    item.UnitOfMeasureName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.Abbreviation.ToUpper().Contains(paginationParams.Filter.ToUpper()));
            }

            var total = (await unitOfMeasure.CountAsync());
            var result = await unitOfMeasure.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);
        }

        public async Task<bool> Update(UnitOfMeasureDto unitOfMeasure)
        {
            var dbObj = await _unitOfWork.UnitOfMeasures.Get(unitOfMeasure.UnitOfMeasureId);
            var objUnitOfMeasure = _mapper.Map(unitOfMeasure, dbObj);
            var obj = _unitOfWork.UnitOfMeasures.Update(objUnitOfMeasure);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int UnitOfMeasureId)
        {
            await ValidateDeleteUnitOfMeasure(UnitOfMeasureId);

            var obj = await _unitOfWork.UnitOfMeasures.Delete(UnitOfMeasureId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        private async Task ValidateDeleteUnitOfMeasure(int UnitOfMeasureId)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.UnitOfMeasureId == UnitOfMeasureId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.UnitOfMeasureUsed);
            }
        }
    }
}
