using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class VendorBankAccountService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorBankAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CRUD Methods

        public async Task<VendorBankAccountDto> AddVendorBankAccount(VendorBankAccountDto vendorBankAccount)
        {

            var newBankAccount = _mapper.Map<BankAccount>(vendorBankAccount);
            var newVendorBankAccount = _mapper.Map<VendorBankAccount>(vendorBankAccount);

            var dbBanckAccount = await _unitOfWork.BankAccounts.Add(newBankAccount);
            newVendorBankAccount.BankAccountId = dbBanckAccount.BankAccoutId;
            var dbVendorBankAccount = await _unitOfWork.VendorBankAccounts.Add(newVendorBankAccount);

            await _unitOfWork.SaveAsync();

            return new VendorBankAccountDto { VendorBankAccountId = dbVendorBankAccount.VendorBankAccountId };
        }

        public bool UpdateVendorBankAccount(VendorBankAccountDto vendorBankAccount)
        {
            var updateBankAccount = _mapper.Map<BankAccount>(vendorBankAccount);
            var updateVendorBankAccount = _mapper.Map<VendorBankAccount>(vendorBankAccount);

            _unitOfWork.BankAccounts.Update(updateBankAccount);
            _unitOfWork.VendorBankAccounts.Update(updateVendorBankAccount);
            _unitOfWork.Save();

            return true;
        }

        public async Task<bool> DeleteVendorBankAccount(int VendorBankAccountId)
        {
            var delVendor = await _unitOfWork.VendorBankAccounts.Delete(VendorBankAccountId);
            await DeleteBankAccount(VendorBankAccountId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteBankAccount(int VendorBankAccountId)
        {
            var delBanckAccount = await _unitOfWork.VendorBankAccounts.Get().Where(o => o.VendorBankAccountId == VendorBankAccountId && o.Active == true).FirstOrDefaultAsync();

            if (delBanckAccount != null) return true;

            await _unitOfWork.BankAccounts.Delete(delBanckAccount.BankAccountId);

            return true;
        }

        #endregion

        #region Validations

        #endregion
    }
}
