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
    public class SubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSubCategory(SubCategory subCategory)
        {
            await _unitOfWork.SubCategories.Add(subCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<SubCategoryDto>> GetSubCategory()
        {
            var obj = await _unitOfWork.SubCategories.Get().ToListAsync();

            return obj.Where(o => o.Active == true).Select(o => {
                return new SubCategoryDto
                {
                    SubCategoryId = o.SubCategoryId,
                    CategoryId= o.CategoryId,
                    NameSubCatagory = o.NameSubCatagory
                };
            });
        }

        public bool UpdateSubCategory(SubCategory subCategory)
        {
            var obj = _unitOfWork.SubCategories.Update(subCategory);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> DeleteSubCategory(SubCategory subCategory)
        {
            await ValidateDeleteSubCategory(subCategory);

            var obj = await _unitOfWork.SubCategories.Delete(subCategory.SubCategoryId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        private async Task ValidateDeleteSubCategory(SubCategory subCategory)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.SubCategoryId == subCategory.SubCategoryId && o.Active == true).ToListAsync();

            if(obj.Any())
            {
                throw new Exception(Messages.SubCategoryUsed);
            }
        }
    }
}
