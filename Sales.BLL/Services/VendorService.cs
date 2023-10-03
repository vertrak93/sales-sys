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
    public class VendorService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CRUD Methods

        public async Task<VendorDto> AddVendor(Vendor vendor)
        {
            var obj = await _unitOfWork.Vendors.Add(vendor);
            await _unitOfWork.SaveAsync();

            return new VendorDto { VendorId = obj.VendorId };
        }

        public async Task<VendorDto> GetVendorAllData(int idVendor)
        {
            var vendorBankAccount = GetVendorBankAccounts(idVendor);
            var vendorPhone = GetVendorPhones(idVendor);
            var vendorAddress = GetVendorAddresses(idVendor);
            var vendor = _unitOfWork.Vendors.Get(idVendor);

            await Task.WhenAll(vendorBankAccount, vendorPhone, vendorAddress, vendor);

            return new VendorDto
            {
                VendorId = vendor.Result.VendorId,
                VendorName = vendor.Result.VendorName,
                TIN = vendor.Result.TIN,
                VendorBankAccount = vendorBankAccount.Result,
                VendorPhone = vendorPhone.Result,
                VendorAddress = vendorAddress.Result
            };
        }

        public async Task<IEnumerable<VendorDto>> GetVendors()
        {
            var vendor = await _unitOfWork.Vendors.Get()
                .Where(o => o.Active == true)
                .Select(o => new VendorDto
                {
                    VendorId = o.VendorId,
                    VendorName = o.VendorName,
                    TIN = o.TIN
                }).ToListAsync();

            return vendor;
        }

        public bool UpdateVendor(Vendor vendor)
        {
            var obj = _unitOfWork.Vendors.Update(vendor);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> DeleteVendor(int VendorId)
        {
            var delVendor = await _unitOfWork.Vendors.Delete(VendorId);
            await DeleteVendorAddress(VendorId);

            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteVendorAddress(int VendorId)
        {
            var delAddress = await _unitOfWork.VendorAddresses.Get().Where( o => o.VendorId == VendorId && o.Active == true).ToListAsync();

            foreach(var address in delAddress)
            {
                await _unitOfWork.VendorAddresses.Delete(address.VendorAddressId);
                await _unitOfWork.Address.Delete(address.AddressId);
            }

            return true;
        }



        public async Task<List<VendorBankAccountDto>> GetVendorBankAccounts(int idVendor)
        {
            var vendorBankAccount = await (from a in _unitOfWork.VendorBankAccounts.Get()
                                           join b in _unitOfWork.BankAccounts.Get() on a.BankAccountId equals b.BankAccoutId
                                           join c in _unitOfWork.Banks.Get() on b.BankId equals c.BankId
                                           where a.Active == true && b.Active == true && c.Active == true
                                           && a.VendorId == idVendor
                                           select new VendorBankAccountDto
                                           {
                                               VendorId = a.VendorId,
                                               BankAccountId = b.BankAccoutId,
                                               AccountNumber = b.AccountNumber,
                                               BankId = c.BankId,
                                               BankName = c.BankName
                                           }).ToListAsync();

            return vendorBankAccount;
        }

        public async Task<List<VendorPhoneDto>> GetVendorPhones(int idVendor)
        {
            var vendorPhone = await (from a in _unitOfWork.VendorPhones.Get()
                                     join b in _unitOfWork.Phones.Get() on a.PhoneId equals b.PhoneId
                                     join c in _unitOfWork.Telephonies.Get() on b.TelephonyId equals c.TelephonyId
                                     where a.Active == true && b.Active == true && c.Active == true
                                     && a.VendorId == idVendor
                                     select new VendorPhoneDto
                                     {
                                         VendorId = a.VendorId,
                                         VendorPhoneId = a.VendorPhoneId,
                                         PhoneId = b.PhoneId,
                                         PhoneNumber = b.PhoneNumber,
                                         Comment = b.Comment,
                                         TelephonyId = c.TelephonyId,
                                         TelephonyName = c.TelephonyName
                                     }).ToListAsync();
            return vendorPhone;
        }

        public async Task<List<VendorAddressDto>> GetVendorAddresses(int idVendor)
        {
            var vendorAddress = await (from a in _unitOfWork.VendorAddresses.Get()
                                       join b in _unitOfWork.Address.Get() on a.AddressId equals b.AddressId
                                       where a.Active == true && b.Active == true
                                       && a.VendorId == idVendor
                                       select new VendorAddressDto
                                       {
                                           VendorId = a.VendorId,
                                           VendorAddressId = a.VendorAddressId,
                                           AddressId = b.AddressId,
                                           AddressDescription = b.AddressDescription
                                       }).ToListAsync();
            return vendorAddress;
        }

        #endregion

        #region Validations

        #endregion

    }
}
