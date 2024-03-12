using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public class ProductService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods
        public async Task<ProductDto> Add(ProductDto product) 
        {

            var objProduct = _mapper.Map<Product>(product);

            await ValidateAddProduct(objProduct);

            var obj = await _unitOfWork.Products.Add(objProduct);
            await _unitOfWork.SaveAsync();

            return new ProductDto { ProductId = obj.ProductId };
        }

        public async Task<(IEnumerable<ProductDto>,int)> Get(PaginationParams paginationParams)
        {
            var products = from a in _unitOfWork.Products.Get()
                      join b in _unitOfWork.Categories.Get() on a.CategoryId equals b.CategoryId
                      join c in _unitOfWork.Brands.Get() on a.BrandId equals c.BrandId
                      join d in _unitOfWork.Presentations.Get() on a.PresentationId equals d.PresentationId
                      join e in _unitOfWork.SubCategories.Get() on new { a.SubCategoryId, Active = true } equals new { SubCategoryId = (int?)e.SubCategoryId, e.Active } into el
                      from subCat in el.DefaultIfEmpty()
                      join f in _unitOfWork.UnitOfMeasures.Get() on a.UnitOfMeasureId equals f.UnitOfMeasureId
                      where a.Active == true
                      && b.Active == true
                      && c.Active == true
                      && d.Active == true
                      && f.Active == true
                      select new ProductDto
                      {
                          ProductId = a.ProductId,
                          CategoryId = a.CategoryId,
                          CategoryName = b.CategoryName,
                          SubCategoryId = a.SubCategoryId,
                          SubCategoryName = subCat.SubCategoryName,
                          BrandId = a.BrandId,
                          BrandName = c.BrandName,
                          PresentationId = a.PresentationId,
                          PresentationName = d.PresentationName,
                          UnitOfMeasureId = a.UnitOfMeasureId,
                          UnitOfMeasureName = f.UnitOfMeasureName,
                          UnitOfMeasureAbbreviation = f.Abbreviation,
                          ProductName = a.ProductName,
                          SKU = a.SKU,
                          MinimumStock = a.MinimumStock,
                          IsContainer = a.IsContainer
                      };

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                products = products.Where(item =>
                    item.CategoryName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.SubCategoryName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.BrandName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.PresentationName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.UnitOfMeasureName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.UnitOfMeasureAbbreviation.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.SKU.ToUpper().Contains(paginationParams.Filter.ToUpper()) );
            }

            var total = (await products.CountAsync());
            var result = await products.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return (result, total);
        }

        public async Task<ProductDto> GetById(int ProductId)
        {
            var obj = from a in _unitOfWork.Products.Get()
                      join b in _unitOfWork.Categories.Get() on a.CategoryId equals b.CategoryId
                      join c in _unitOfWork.Brands.Get() on a.BrandId equals c.BrandId
                      join d in _unitOfWork.Presentations.Get() on a.PresentationId equals d.PresentationId
                      join e in _unitOfWork.SubCategories.Get() on new { a.SubCategoryId, Active = true } equals new { SubCategoryId = (int?)e.SubCategoryId, e.Active } into el
                      from subCat in el.DefaultIfEmpty()
                      where a.Active == true
                      && b.Active == true
                      && c.Active == true
                      && d.Active == true
                      && a.ProductId == ProductId
                      select new ProductDto
                      {
                          ProductId = a.ProductId,
                          CategoryId = a.CategoryId,
                          CategoryName = b.CategoryName,
                          SubCategoryId = a.SubCategoryId,
                          SubCategoryName = subCat.SubCategoryName,
                          BrandId = a.BrandId,
                          BrandName = c.BrandName,
                          PresentationId = a.PresentationId,
                          PresentationName = d.PresentationName,
                          ProductName = a.ProductName,
                          SKU = a.SKU,
                          MinimumStock = a.MinimumStock,
                          IsContainer = a.IsContainer
                      };

            var dbObj = await obj.FirstOrDefaultAsync();

            return dbObj;
        }

        public async Task<bool> Update(ProductDto product)
        {
            var dbObj = await _unitOfWork.Products.Get(product.ProductId);
            var objProduct = _mapper.Map(product, dbObj);
            var obj = _unitOfWork.Products.Update(objProduct);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int ProductId)
        {
            await ValidateDeleteProduct(ProductId);
            var obj = await _unitOfWork.Products.Delete(ProductId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        #endregion

        #region Validation Methods
        public async Task ValidateAddProduct(Product newProduct)
        {
            var existSKU = await _unitOfWork.Products.Get().Where( o =>
                o.Active == true &&
                o.SKU == newProduct.SKU
            ).ToListAsync();

            if (existSKU.Any()) { throw new Exception(Messages.SKUDontAvalible); }
        }

        public async Task ValidateDeleteProduct(int ProductId)
        {
            var existVendorProduct = await _unitOfWork.VendorProducts.Get().Where( o=> o.ProductId == ProductId && o.Active ).ToListAsync();
            if (existVendorProduct.Any()) { throw new Exception(Messages.VendorProductUsed); }
        }
        #endregion

    }
}
