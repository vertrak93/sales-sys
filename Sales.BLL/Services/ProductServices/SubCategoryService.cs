using AutoMapper;
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
        private readonly IMapper _mapper;

        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(SubCategoryDto subCategory)
        {
            var objSubCategory = _mapper.Map<SubCategory>(subCategory);
            await _unitOfWork.SubCategories.Add(objSubCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<SubCategoryDto>> Get()
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

        public async Task<bool> Update(SubCategoryDto subCategory)
        {
            var dbObj = await _unitOfWork.SubCategories.Get(subCategory.SubCategoryId);
            var objSubCategory = _mapper.Map(subCategory, dbObj);
            var obj = _unitOfWork.SubCategories.Update(objSubCategory);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int SubCategoryId)
        {
            await ValidateDeleteSubCategory(SubCategoryId);

            var obj = await _unitOfWork.SubCategories.Delete(SubCategoryId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        private async Task ValidateDeleteSubCategory(int SubCategoryId)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.SubCategoryId == SubCategoryId && o.Active == true).ToListAsync();

            if(obj.Any())
            {
                throw new Exception(Messages.SubCategoryUsed);
            }
        }
    }
}
