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
    public class TelephonyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TelephonyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(TelephonyDto telephony)
        {
            var objTelephony = _mapper.Map<Telephony>(telephony);
            await _unitOfWork.Telephonies.Add(objTelephony);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TelephonyDto>> Get()
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

        public async Task<bool> Update(TelephonyDto telephony)
        {
            var dbObj = await _unitOfWork.Telephonies.Get(telephony.TelephonyId);
            var objTelephony = _mapper.Map(telephony, dbObj);
            var obj = _unitOfWork.Telephonies.Update(objTelephony);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task<bool> Delete(int TelephonyId)
        {
            await ValidateDeleteTelephony(TelephonyId);
            var obj = await _unitOfWork.Banks.Delete(TelephonyId);
            await _unitOfWork.SaveAsync();
            return obj;
        }

        public async Task ValidateDeleteTelephony(int TelephonyId)
        {
            var obj = await _unitOfWork.Phones.Get().Where(o => o.TelephonyId == TelephonyId && o.Active == true).ToListAsync();

            if (obj.Any()){ throw new Exception(Messages.TelephonyUsedPhone); }
        }
    }
}
