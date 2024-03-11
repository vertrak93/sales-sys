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
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(CategoryDto category)
        {
            var objCategory = _mapper.Map<Category>(category);
            await _unitOfWork.Categories.Add(objCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<CategoryDto>, int)> Get(PaginationParams paginationParams)
        {
            var category = _unitOfWork.Categories.Get()
                    .Where(o => o.Active == true)
                    .Select(o => new CategoryDto
                    {
                        CategoryId = o.CategoryId,
                        CategoryName = o.CategoryName
                    });

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                category = category.Where(item =>
                    item.CategoryName.ToUpper().Contains(paginationParams.Filter.ToUpper()));
            }

            var total = (await category.CountAsync());
            var result = await category.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);
        }

        public async Task<bool> Update(CategoryDto category)
        {
            var dbObj = await _unitOfWork.Categories.Get(category.CategoryId);
            var objCategory = _mapper.Map(category, dbObj);
            var obj = _unitOfWork.Categories.Update(objCategory);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int CategoryId)
        {
            await ValidateDeleteCategory(CategoryId);
            var obj = await _unitOfWork.Categories.Delete(CategoryId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteCategory(int CategoryId)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.CategoryId == CategoryId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.CategoryUsed);
            }
        }
    }
}
