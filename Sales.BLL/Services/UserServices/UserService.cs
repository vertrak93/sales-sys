using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils;
using Sales.Utils.Constants;
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

        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await _unitOfWork.Users.Get().Where(o => o.Active == true).Select(el => new UserDto
            {
                Username = el.Username,
                FisrtName = el.FisrtName,
                LastName = el.LastName,
                Email = el.Email,
            }).ToListAsync();

            return users;
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
            var dbObj = await _unitOfWork.Users.Get().Where(o => o.Username.ToUpper() == patchObj.Username.ToUpper()).FirstOrDefaultAsync();
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
