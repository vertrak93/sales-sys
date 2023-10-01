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

        public async Task<VendorAddressDto> AddVendorAddress(VendorAddressDto vendorAddress)
        {
            
            var newAddress = _mapper.Map<Address>(vendorAddress);
            var newVendorAddress = _mapper.Map<VendorAddress>(vendorAddress);

            var dbAdrress = await _unitOfWork.Address.Add(newAddress);
            vendorAddress.AddressId = dbAdrress.AddressId;
            var dbVendorAddress = await _unitOfWork.VendorAddresses.Add(newVendorAddress);

            await _unitOfWork.SaveAsync();

            return new VendorAddressDto { VendorAddressId = dbVendorAddress.VendorAddressId };
        }

        public bool UpdateVendorAddress(VendorAddressDto vendorAddress)
        {
            var updateAddress = _mapper.Map<Address>(vendorAddress);
            var updateVendorAddress = _mapper.Map<VendorAddress>(vendorAddress);

            _unitOfWork.Address.Update(updateAddress);
            _unitOfWork.VendorAddresses.Update(updateVendorAddress);
            _unitOfWork.Save();

            return true;
        }

        public async Task<bool> DeleteVendorAddress(VendorAddressDto vendorAddress)
        {
            var delVendor = await _unitOfWork.VendorAddresses.Delete(vendorAddress.VendorAddressId);
            await DeleteAddress(vendorAddress.VendorAddressId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteAddress(int VendorAddressId)
        {
            var delAddress = await _unitOfWork.VendorAddresses.Get().Where(o => o.VendorAddressId == VendorAddressId).ToListAsync();

            foreach (var address in delAddress)
            {
                await _unitOfWork.VendorAddresses.Delete(address.AddressId);
            }

            return true;
        }

        #endregion

        #region Validations

        #endregion
    }
}
