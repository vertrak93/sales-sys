using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class VendorProductService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods

        public async Task<IEnumerable<VendorProductDto>> GetByProduct(int ProductId)
        {
            var obj = from a in _unitOfWork.VendorProducts.Get()
                      join b in _unitOfWork.Products.Get() on a.ProductId equals b.ProductId
                      join c in _unitOfWork.Vendors.Get() on a.VendorId equals c.VendorId
                      where a.Active == true
                      && b.Active == true
                      && c.Active == true
                      && a.ProductId == ProductId
                      select new VendorProductDto
                      {
                          VendorProductId = a.VendorProductId,
                          ProductId = a.ProductId,
                          ProductName = b.ProductName,
                          VendorId = c.VendorId,
                          VendorName = c.VendorName
                      };

            var dbObj = await obj.ToListAsync();

            return dbObj;
        }

        public async Task<IEnumerable<VendorProductDto>> GetByVendor(int VendorId)
        {
            var obj = from a in _unitOfWork.VendorProducts.Get()
                      join b in _unitOfWork.Products.Get() on a.ProductId equals b.ProductId
                      join c in _unitOfWork.Vendors.Get() on a.VendorId equals c.VendorId
                      where a.Active == true
                      && b.Active == true
                      && c.Active == true
                      && c.VendorId == VendorId
                      select new VendorProductDto
                      {
                          VendorProductId = a.VendorProductId,
                          ProductId = a.ProductId,
                          ProductName = b.ProductName,
                          VendorId = c.VendorId,
                          VendorName = c.VendorName
                      };

            var dbObj = await obj.ToListAsync();

            return dbObj;
        }

        public async Task<VendorProductDto> Add(VendorProductDto vendorProduct)
        {
            var newVendorProduct = _mapper.Map<VendorProduct>(vendorProduct);

            var dbVendorProduct = await _unitOfWork.VendorProducts.Add(newVendorProduct);

            await _unitOfWork.SaveAsync();

            return new VendorProductDto { VendorProductId = dbVendorProduct.VendorProductId };
        }

        public async Task<bool> Update(VendorProductDto vendorProduct)
        {
            var dbVendorProduct = await _unitOfWork.VendorProducts.Get(vendorProduct.VendorProductId);
            var updateVendorProduct = _mapper.Map(vendorProduct, dbVendorProduct);

            _unitOfWork.VendorProducts.Update(updateVendorProduct);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(int VendorProductId)
        {
            var delVendor = await _unitOfWork.VendorProducts.Delete(VendorProductId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        #endregion
    }
}
