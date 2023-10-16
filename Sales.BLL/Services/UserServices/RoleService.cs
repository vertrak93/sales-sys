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

namespace Sales.BLL.Services.UserServices
{
    public class RoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> Get()
        {
            var obj = await _unitOfWork.Roles.Get().Where(o => o.Active == true).ToListAsync();

            return obj.Select(o => {
                return new RoleDto
                {
                    RoleId = o.RoleId,
                    RoleName = o.RoleName
                };
            });
        }

        public async Task<List<RoleDto>> GetRolesByUserId(int id)
        {
            var objRoles = await (from a in _unitOfWork.UserRoles.Get()
                                  join b in _unitOfWork.Roles.Get() on new { a.RoleId, Active = true } equals new { b.RoleId, Active = (bool)b.Active }
                                  join c in _unitOfWork.Users.Get() on a.UserId equals c.UserId
                                  where c.UserId == id &&
                                        a.Active == true
                                        && c.Active == true
                                  select new RoleDto
                                  {
                                      RoleId = a.RoleId,
                                      RoleName = b.RoleName,

                                  }).ToListAsync(); ;

            return objRoles;

        }

    }
}
