using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.DTOs.UserDtos;
using Sales.Models;
using Sales.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.UserServices
{
    public class UserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(UserRoleDto newObj)
        {
            var objuserRole = _mapper.Map<UserRole>(newObj);
            await _unitOfWork.UserRoles.Add(objuserRole);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserRoleDto>> GetByUserIdAsigned(int UserId)
        {
            var obj = await (from a in _unitOfWork.Roles.Get()
                             join b in _unitOfWork.UserRoles.Get() on new { Active = true, RoleId = a.RoleId } equals new { b.Active, b.RoleId } into lb
                             from c in lb.DefaultIfEmpty()
                             where c.UserId == UserId
                             && a.Active == true
                             select new UserRoleDto
                             {
                                 UserRoleId = c.UserId,
                                 UserId = c.UserId,
                                 RoleId = a.RoleId,
                                 RoleName = a.RoleName
                             }).ToListAsync();

            return obj;
        }

        public async Task<IEnumerable<UserRoleDto>> GetByUserId(int UserId)
        {
            var obj = await (from a in _unitOfWork.UserRoles.Get()
                             join b in _unitOfWork.Roles.Get() on a.RoleId equals b.RoleId
                             where a.UserId == UserId
                             && a.Active == true
                             && b.Active == true
                             select new UserRoleDto
                             {
                                 UserRoleId = a.UserRoleId,
                                 UserId = a.UserId,
                                 RoleId = b.RoleId,
                                 RoleName = b.RoleName
                             }).ToListAsync();

            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _unitOfWork.UserRoles.Delete(id);
            await _unitOfWork.SaveAsync();
            return obj;
        }

    }
}
