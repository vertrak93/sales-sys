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
    public class VendorAddressService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorAddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods

        public async Task<VendorAddressDto> Add(VendorAddressDto vendorAddress)
        {
            
            var newAddress = _mapper.Map<Address>(vendorAddress);
            var newVendorAddress = _mapper.Map<VendorAddress>(vendorAddress);

            var dbAdrress = await _unitOfWork.Address.Add(newAddress);
            newVendorAddress.AddressId = dbAdrress.AddressId;
            var dbVendorAddress = await _unitOfWork.VendorAddresses.Add(newVendorAddress);

            await _unitOfWork.SaveAsync();

            return new VendorAddressDto { VendorAddressId = dbVendorAddress.VendorAddressId };
        }

        public async Task<bool> Update(VendorAddressDto vendorAddress)
        {            
            var dbAddress = await _unitOfWork.Address.Get(vendorAddress.AddressId);
            var dbVendorAdress = await _unitOfWork.VendorAddresses.Get(vendorAddress.AddressId);

            var updateAddress = _mapper.Map(vendorAddress, dbAddress);
            var updateVendorAddress = _mapper.Map(vendorAddress, dbVendorAdress);

            _unitOfWork.Address.Update(updateAddress);
            _unitOfWork.VendorAddresses.Update(updateVendorAddress);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(int VendorAddressId)
        {
            var delVendor = await DeleteVendorAddress(VendorAddressId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteVendorAddress(int VendorAddressId)
        {
            var delVendor = await _unitOfWork.VendorAddresses.Delete(VendorAddressId);
            var delAddress = await DeleteAddress(VendorAddressId);
            return delVendor;
        }

        public async Task<bool> DeleteAddress(int VendorAddressId)
        {
            var delAddress = await _unitOfWork.VendorAddresses.Get().Where(o => o.VendorAddressId == VendorAddressId && o.Active == true).FirstOrDefaultAsync();

            if (delAddress == null) return true;

            await _unitOfWork.Address.Delete(delAddress.AddressId);

            return true;
        }

        #endregion

        #region Validations

        #endregion
    }
}
