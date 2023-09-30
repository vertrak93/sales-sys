using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils.Constants;
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

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBrand(Brand brand)
        {
            await _unitOfWork.Brands.Add(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var obj = await _unitOfWork.Brands.Get().Where(o => o.Active == true).ToListAsync();

            return obj.Select(o => { return new BrandDto 
                { 
                    BrandId = o.BrandId, 
                    BrandName = o.BrandName 
                }; 
            });
        }

        public bool UpdateBrand(Brand brand)
        {
            var obj = _unitOfWork.Brands.Update(brand); 
            return obj;
        }

        public async Task<bool> DeleteBrand(Brand brand) 
        {
            await ValidateDeleteBrand(brand);
            var obj = await _unitOfWork.Brands.Delete(brand.BrandId);
            return obj;
        }

        public async Task ValidateDeleteBrand(Brand brand)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.BrandId == brand.BrandId && o.Active == true).ToListAsync();

            if(obj.Any())
            {
                throw new Exception(Messages.BrandUsed);
            }
        }
    }
}
