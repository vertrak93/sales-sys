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

        public async Task<VendorBankAccountDto> Add(VendorBankAccountDto vendorBankAccount)
        {

            var newBankAccount = _mapper.Map<BankAccount>(vendorBankAccount);
            var newVendorBankAccount = _mapper.Map<VendorBankAccount>(vendorBankAccount);

            await _unitOfWork.BankAccounts.Add(newBankAccount);
            newVendorBankAccount.BankAccount = newBankAccount;
            var dbVendorBankAccount = await _unitOfWork.VendorBankAccounts.Add(newVendorBankAccount);

            await _unitOfWork.SaveAsync();

            return new VendorBankAccountDto { VendorBankAccountId = dbVendorBankAccount.VendorBankAccountId };
        }

        public async Task<bool> Update(VendorBankAccountDto vendorBankAccount)
        {
            var dbBankAccount = await _unitOfWork.BankAccounts.Get(vendorBankAccount.BankAccountId);
            var dbVendorBanckAccount = await _unitOfWork.VendorBankAccounts.Get(vendorBankAccount.VendorBankAccountId);

            var updateBankAccount = _mapper.Map(vendorBankAccount, dbBankAccount);
            var updateVendorBankAccount = _mapper.Map(vendorBankAccount, dbVendorBanckAccount);

            _unitOfWork.BankAccounts.Update(updateBankAccount);
            _unitOfWork.VendorBankAccounts.Update(updateVendorBankAccount);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(int VendorBankAccountId)
        {
            var delVendor  = await DeleteVendorBankAccount(VendorBankAccountId);
            await _unitOfWork.SaveAsync();
            return delVendor;
        }

        public async Task<bool> DeleteVendorBankAccount(int VendorBankAccountId)
        {
            var delVendor = await _unitOfWork.VendorBankAccounts.Delete(VendorBankAccountId);
            var delBankAccount = await DeleteBankAccount(VendorBankAccountId);
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
