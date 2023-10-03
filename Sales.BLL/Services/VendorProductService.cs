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

        public async Task<VendorProductDto> AddVendorProduct(VendorProductDto vendorProduct)
        {
            var newVendorProduct = _mapper.Map<VendorProduct>(vendorProduct);

            var dbVendorProduct = await _unitOfWork.VendorProducts.Add(newVendorProduct);

            await _unitOfWork.SaveAsync();

            return new VendorProductDto { VendorProductId = dbVendorProduct.VendorProductId };
        }

        public bool UpdateVendorProduct(VendorProductDto vendorProduct)
        {
            var updateVendorProduct = _mapper.Map<VendorProduct>(vendorProduct);

            _unitOfWork.VendorProducts.Update(updateVendorProduct);
            _unitOfWork.Save();

            return true;
        }

        public async Task<bool> DeleteVendorProduct(int VendorProductId)
        {
            var delVendor = await _unitOfWork.VendorProducts.Delete(VendorProductId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        #endregion
    }
}
