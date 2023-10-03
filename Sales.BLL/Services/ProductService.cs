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

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CRUD Methods
        public async Task<ProductDto> AddProduct(Product product) 
        {
            await ValidateAddProduct(product);

            var obj = await _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveAsync();

            return new ProductDto { ProductId = obj.ProductId };
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
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

        public bool UpdateProduct(Product product)
        {
            var obj = _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            await ValidateDeleteProduct(product);
            var obj = await _unitOfWork.Products.Delete(product.ProductId);
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

        public async Task ValidateDeleteProduct(Product product)
        {
            var existVendorProduct = await _unitOfWork.VendorProducts.Get().Where( o=> o.ProductId== product.ProductId ).ToListAsync();
            if (existVendorProduct.Any()) { throw new Exception(Messages.VendorProductUsed); }
        }
        #endregion

    }
}
