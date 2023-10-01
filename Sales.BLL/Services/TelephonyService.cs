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
    public class TelephonyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TelephonyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddTelephony(Telephony telephony)
        {
            await _unitOfWork.Telephonies.Add(telephony);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TelephonyDto>> GetBanks()
        {
            var obj = await _unitOfWork.Telephonies.Get().Where(o => o.Active == true).ToListAsync();

            return obj.Select(o => {
                return new TelephonyDto
                {
                    TelephonyId = o.TelephonyId,
                    TelephonyName = o.TelephonyName
                };
            });
        }

        public bool UpdateTelephony(Telephony telephony)
        {
            var obj = _unitOfWork.Telephonies.Update(telephony);
            _unitOfWork.Save();
            return obj;
        }

        public async Task<bool> DeleteTelephony(Telephony telephony)
        {
            await ValidateDeleteTelephony(telephony);
            var obj = await _unitOfWork.Banks.Delete(telephony.TelephonyId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteTelephony(Telephony telephony)
        {
            var obj = await _unitOfWork.Phones.Get().Where(o => o.TelephonyId == telephony.TelephonyId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.TelephonyUsedPhone);
            }
        }
    }
}
