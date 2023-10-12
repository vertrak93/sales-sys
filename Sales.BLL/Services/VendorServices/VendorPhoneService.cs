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
    public class VendorPhoneService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorPhoneService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods

        public async Task<VendorPhoneDto> Add(VendorPhoneDto vendorPhone)
        {
            var newPhone = _mapper.Map<Phone>(vendorPhone);
            var newVendorPhone = _mapper.Map<VendorPhone>(vendorPhone);

            var dbPhone = await _unitOfWork.Phones.Add(newPhone);
            newVendorPhone.PhoneId = dbPhone.PhoneId;
            var dbVendorPhone = await _unitOfWork.VendorPhones.Add(newVendorPhone);

            await _unitOfWork.SaveAsync();

            return new VendorPhoneDto { VendorPhoneId = dbVendorPhone.VendorPhoneId };
        }

        public async Task<bool> Update(VendorPhoneDto vendorPhone)
        {
            var dbPhone = await _unitOfWork.Phones.Get(vendorPhone.PhoneId);
            var dbVendorPhone = await _unitOfWork.VendorPhones.Get(vendorPhone.VendorPhoneId);

            var updatePhone = _mapper.Map(vendorPhone,dbPhone);
            var updateVendorPhone = _mapper.Map(vendorPhone, dbVendorPhone);

            _unitOfWork.Phones.Update(updatePhone);
            _unitOfWork.VendorPhones.Update(updateVendorPhone);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(int VendorPhoneId)
        {
            var delVendor = await DeleteVendorPhone(VendorPhoneId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteVendorPhone(int VendorPhoneId)
        {
            var delVendor = await _unitOfWork.VendorPhones.Delete(VendorPhoneId);
            await DeletePhone(VendorPhoneId);
            return delVendor;
        }

        public async Task<bool> DeletePhone(int VendorPhoneId)
        {
            var delPhone = await _unitOfWork.VendorPhones.Get().Where(o => o.VendorPhoneId == VendorPhoneId && o.Active == true).FirstOrDefaultAsync();

            if (delPhone != null) return true;

            await _unitOfWork.Phones.Delete(delPhone.PhoneId);

            return true;
        }

        #endregion

        #region Validations

        #endregion
    }
}
