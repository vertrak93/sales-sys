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

        public async Task<(IEnumerable<SubCategoryDto>,int)> Get(PaginationParams paginationParams)
        {
            var subCategories = from a in _unitOfWork.SubCategories.Get()
                                join b in _unitOfWork.Categories.Get() on a.CategoryId equals b.CategoryId
                                where a.Active == true && b.Active == true
                                select new SubCategoryDto
                                {
                                    SubCategoryId = a.SubCategoryId,
                                    SubCategoryName = a.SubCategoryName,
                                    CategoryId = b.CategoryId,
                                    CategoryName = b.CategoryName
                                };

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                subCategories = subCategories.Where(item =>
                    item.SubCategoryName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.CategoryName.ToUpper().Contains(paginationParams.Filter.ToUpper()) );
            }

            var total = (await subCategories.CountAsync());
            var result = await subCategories.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);
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
