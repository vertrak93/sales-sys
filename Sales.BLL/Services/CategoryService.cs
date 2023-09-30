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
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCategory(Category category)
        {
            await _unitOfWork.Categories.Add(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var obj = await _unitOfWork.Categories.Get().ToListAsync();

            return obj.Where(o => o.Active == true).Select(o => {
                return new CategoryDto
                {
                    CategoryId = o.CategoryId,
                    CategoryName = o.CategoryName
                };
            });
        }

        public bool UpdateCategory(Category category)
        {
            var obj = _unitOfWork.Categories.Update(category);
            return obj;
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            await ValidateDeleteCategory(category);
            var obj = await _unitOfWork.Categories.Delete(category.CategoryId);
            return obj;
        }

        public async Task ValidateDeleteCategory(Category category)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.CategoryId == category.CategoryId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.CategoryUsed);
            }
        }
    }
}
