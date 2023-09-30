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
    public class BankService
    {

        private readonly IUnitOfWork _unitOfWork;

        public BankService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBank(Bank bank)
        {
            await _unitOfWork.Banks.Add(bank);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BankDto>> GetBanks()
        {
            var obj = await _unitOfWork.Banks.Get().Where(o => o.Active == true).ToListAsync();

            return obj.Select(o => {
                return new BankDto
                {
                    BankId = o.BankId,
                    BankName = o.BankName
                };
            });
        }

        public bool UpdateBank(Bank bank)
        {
            var obj = _unitOfWork.Banks.Update(bank);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> DeleteBank(Bank bank)
        {
            await ValidateDeleteBank(bank);
            var obj = await _unitOfWork.Banks.Delete(bank.BankId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteBank(Bank bank)
        {
            var obj = await _unitOfWork.BankAccounts.Get().Where(o => o.BankId == bank.BankId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.BankUsedAccount);
            }
        }
    }
}
