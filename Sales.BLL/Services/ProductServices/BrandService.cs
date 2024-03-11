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
    public class BrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(BrandDto brand)
        {
            var objBrand = _mapper.Map<Brand>(brand);
            await _unitOfWork.Brands.Add(objBrand);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<BrandDto>, int)> Get(PaginationParams paginationParams)
        {
            var brands = _unitOfWork.Brands.Get()
                    .Where(ol => ol.Active == true)
                    .Select(o => new BrandDto
                    {
                        BrandId = o.BrandId,
                        BrandName = o.BrandName
                    });

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                brands = brands.Where(item =>
                    item.BrandName.ToUpper().Contains(paginationParams.Filter.ToUpper()));
            }

            var total = (await brands.CountAsync());
            var result = await brands.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);
        }

        public async Task<BrandDto> Get(int id)
        {
            var dbDbj = await _unitOfWork.Brands.Get(id);
            var obj = _mapper.Map<BrandDto>(dbDbj);
            return obj;
        }

        public async Task<bool> Update(BrandDto brand)
        {
            var dbObj = await _unitOfWork.Brands.Get(brand.BrandId);
            var objBrand = _mapper.Map(brand, dbObj);
            var obj = _unitOfWork.Brands.Update(objBrand);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int BrandId) 
        {
            await ValidateDeleteBrand(BrandId);
            var obj = await _unitOfWork.Brands.Delete(BrandId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteBrand(int BrandId)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.BrandId == BrandId && o.Active == true).ToListAsync();

            if(obj.Any())
            {
                throw new Exception(Messages.BrandUsed);
            }
        }
    }
}
