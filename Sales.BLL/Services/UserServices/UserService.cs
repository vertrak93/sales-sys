using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils;
using Sales.Utils.Constants;
using Sales.Utils.UtilsDto;
using System;
using System.Text.RegularExpressions;

namespace Sales.BLL.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<UserDto>, int)> Get(PaginationParams paginationParams)
        {
            var users = _unitOfWork.Users.Get()
                .Select(el => new UserDto
                {
                    UserId = el.UserId,
                    Username = el.Username,
                    FisrtName = el.FisrtName,
                    LastName = el.LastName,
                    Email = el.Email,
                    Active = el.Active
                });

            if (!string.IsNullOrEmpty(paginationParams.Filter))
            {
                users = users.Where(item =>
                    item.Username.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.FisrtName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.LastName.ToUpper().Contains(paginationParams.Filter.ToUpper()) ||
                    item.Email.ToUpper().Contains(paginationParams.Filter.ToUpper()));
            }

            var total = (await users.CountAsync());
            var result = await users.Skip(paginationParams.Start)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return (result, total);
        }

        public async Task<bool> Add(UserDto newObj)
        {
            await ValidatePostUser(newObj);
            var obj = _mapper.Map<User>(newObj);
            obj.Password = Cryptography.GetSHA256(newObj.Password);
            var result = await _unitOfWork.Users.Add(obj);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Update(UserDto patchObj)
        {
            ValidateEmailFormat(patchObj);
            var dbObj = await _unitOfWork.Users.Get().Where(o => o.UserId == patchObj.UserId).FirstOrDefaultAsync();
            patchObj.Password = dbObj.Password;
            var obj = _mapper.Map(patchObj, dbObj);
            var resutl = _unitOfWork.Users.Update(obj);

            await _unitOfWork.SaveAsync();

            return resutl;
        }

        public async Task ChangePassword(UserDto user)
        {
            ValidatePasswordFormat(user);
            await ValidateExistingPassword(user);
            var objUser = _unitOfWork.Users.Get().Where(obj => obj.Active == true && obj.UserId == user.UserId).FirstOrDefault();
            if (objUser != null)
            {
                var newPassword = Cryptography.GetSHA256(user.Password);
                objUser.Password = newPassword;
                _unitOfWork.Users.Update(objUser);
                await _unitOfWork.SaveAsync();
            }

        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<bool> Activate(int id)
        {
            var result = await _unitOfWork.Users.Activate(id);
            await _unitOfWork.SaveAsync();
            return result;
        }

        #region Validations

        public async Task ValidatePostUser(UserDto user)
        {
            ValidatePasswordFormat(user);
            ValidateEmailFormat(user);
            await ValidateExistingUser(user);
            await ValidateExistingEmail(user);
        }

        public async Task ValidateExistingUser(UserDto user)
        {
            var objUser = await _unitOfWork.Users.Get().Where(obj => obj.Active == true && obj.Username.ToUpper() == user.Username).ToListAsync();
            if (objUser.Count > 0) { throw new Exception(Messages.ExistingUserName); }
        }

        public async Task ValidateExistingEmail(UserDto user)
        {
            var objMail = await _unitOfWork.Users.Get().Where(obj => obj.Active == true && obj.Email.ToUpper() == user.Email).ToListAsync();
            if (objMail.Count > 0) { throw new Exception(Messages.ExistingMail); }
        }

        public void ValidatePasswordFormat(UserDto user)
        {
            var matchRegex = Regex.Match(user.Password, RegularExpressions.PasswordRegx);
            if (!matchRegex.Success) { throw new Exception(Messages.FormatPasswordNotMatch); }
        }

        public void ValidateEmailFormat(UserDto user)
        {
            var matchRegex = Regex.Match(user.Email, RegularExpressions.EmailRegx);
            if (!matchRegex.Success) { throw new Exception(Messages.FormatEmailNotMatch); }
        }

        public async Task ValidateExistingPassword(UserDto user)
        {
            var objUsuario = await _unitOfWork.Users.Get().Where(obj => obj.UserId == user.UserId).FirstOrDefaultAsync();
            if (objUsuario != null)
            {
                var newPassword = Cryptography.GetSHA256(user.Password);
                if (newPassword == objUsuario.Password) { throw new Exception(Messages.ItsSamePassword); }
            }
        }

        #endregion

    }
}
