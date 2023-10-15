using AutoMapper;
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
    public class VendorService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VendorAddressService _vendorAddressService;
        private readonly VendorBankAccountService _vendorBankAccountService;
        private readonly VendorPhoneService _vendorPhoneService;

        public VendorService(IUnitOfWork unitOfWork, IMapper mapper, VendorAddressService vendorAddressService, VendorBankAccountService vendorBankAccountService, VendorPhoneService vendorPhoneService)
        {
            _unitOfWork = unitOfWork;
            _vendorAddressService = vendorAddressService;
            _vendorBankAccountService = vendorBankAccountService;
            _vendorPhoneService = vendorPhoneService;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods

        public async Task<VendorDto> Add(VendorDto vendor)
        {
            var newObj = _mapper.Map<Vendor>(vendor);
            var obj = await _unitOfWork.Vendors.Add(newObj);
            await _unitOfWork.SaveAsync();

            return new VendorDto { VendorId = obj.VendorId };
        }

        public async Task<VendorDto> GetById(int idVendor)
        {
            var vendorBankAccount = await GetVendorBankAccounts(idVendor);
            var vendorPhone = await GetVendorPhones(idVendor);
            var vendorAddress = await GetVendorAddresses(idVendor);
            var vendor = await _unitOfWork.Vendors.Get(idVendor);

            return new VendorDto
            {
                VendorId = vendor.VendorId,
                VendorName = vendor.VendorName,
                TIN = vendor.TIN,
                VendorBankAccount = vendorBankAccount,
                VendorPhone = vendorPhone,
                VendorAddress = vendorAddress
            };
        }

        public async Task<IEnumerable<VendorDto>> Get()
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

        public async Task<bool> Update(VendorDto vendor)
        {
            var dbObj = await _unitOfWork.Vendors.Get(vendor.VendorId);
            var updateVendor = _mapper.Map(vendor, dbObj);
            var obj = _unitOfWork.Vendors.Update(updateVendor);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int VendorId)
        {
            await ValidateDeleteVendorProduct(VendorId);

            var delVendor = await _unitOfWork.Vendors.Delete(VendorId);

            await DeleteVendorAddress(VendorId);
            await DeleteVendorBankAccount(VendorId);
            await DeleteVendorPhone(VendorId);

            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteVendorAddress(int VendorId)
        {
            var delAddress = await _unitOfWork.VendorAddresses.Get().Where(o => o.VendorId == VendorId && o.Active == true).ToListAsync();

            foreach (var address in delAddress)
            {
                await _vendorAddressService.DeleteVendorAddress(address.VendorAddressId);
            }
            return true;
        }

        public async Task<bool> DeleteVendorBankAccount(int VendorId)
        {
            var delBankAccount = await _unitOfWork.VendorBankAccounts.Get().Where(o => o.VendorId == VendorId && o.Active == true).ToListAsync();

            foreach (var bankAccount in delBankAccount)
            {
                await _vendorBankAccountService.DeleteVendorBankAccount(bankAccount.VendorBankAccountId);
            }
            return true;
        }

        public async Task<bool> DeleteVendorPhone(int VendorId)
        {
            var delPhone = await _unitOfWork.VendorPhones.Get().Where(o => o.VendorId == VendorId && o.Active == true).ToListAsync();

            foreach (var phone in delPhone)
            {
                await _vendorPhoneService.DeleteVendorPhone(phone.VendorPhoneId);
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

        public async Task ValidateDeleteVendorProduct(int idVendor)
        {
            var obj = await _unitOfWork.VendorProducts.Get().Where(o => o.VendorId == idVendor && o.Active == true).CountAsync();

            if(obj > 0)
            {
                throw new Exception(Messages.ProductUseVendorDelete);
            }

            return;
        }

        #endregion

    }
}
