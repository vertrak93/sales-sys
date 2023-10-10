using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public async Task<IEnumerable<ProductDto>> Get()
        {
            var obj = await _unitOfWork.Products.Get().Where(o=> o.Active == true).ToListAsync();

            return obj.Select(o => new ProductDto
            {
                ProductId = o.ProductId,
                CategoryId = o.CategoryId,
                SubCategoryId = o.SubCategoryId,
                BrandId = o.BrandId,
                PresentationId = o.PresentationId,
                ProductName = o.ProductName,
                SKU = o.SKU,
                MinimumStock = o.MinimumStock,
                IsContainer = o.IsContainer
            });
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
