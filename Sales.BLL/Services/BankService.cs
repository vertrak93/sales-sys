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
    public class BankService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(BankDto bank)
        {
            var objBank = _mapper.Map<Bank>(bank);
            await _unitOfWork.Banks.Add(objBank);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BankDto>> Get()
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

        public async Task<bool> Update(BankDto bank)
        {
            var dbObj = await _unitOfWork.Banks.Get(bank.BankId);
            var objBank = _mapper.Map(bank, dbObj);
            var obj = _unitOfWork.Banks.Update(objBank);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> Delete(int BankId)
        {
            await ValidateDeleteBank(BankId);
            var obj = await _unitOfWork.Banks.Delete(BankId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteBank(int BankId)
        {
            var obj = await _unitOfWork.BankAccounts.Get().Where(o => o.BankId == BankId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.BankUsedAccount);
            }
        }
    }
}
