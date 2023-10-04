using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils;
using Sales.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.UserServices
{
    public class AuthenticationService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Metodos

        public async Task<TokenDto> Authenticate(AuthenticateDto auth, string keyJwt)
        {

            var user = await ValidateLogin(auth);
            var roles = await GetRolesUser(user);

            var AccessToken = TokenGenerator.Instance().GenerateJWTToken(user, roles, keyJwt);
            var RefreshToken = await CreateRefreshToken(user.Username);

            return new TokenDto
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
            };
        }

        public async Task<string> CreateRefreshToken(string userName)
        {
            var user = await _unitOfWork.Users.Get().Where(obj => obj.Username == userName).FirstOrDefaultAsync();
            DateTime now = DateTime.UtcNow;

            RefreshToken refreshToken = new RefreshToken();
            string token = TokenGenerator.Instance().GenerateRefreshToken();

            refreshToken.UserId = user.UserId;
            refreshToken.Token = token;
            refreshToken.Expiration = now.AddHours(5);
            refreshToken.Active = true;

            await _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.SaveAsync();

            return token;
        }

        public async Task<UserDto> ValidateLogin(AuthenticateDto auth)
        {
            var objUser = await _unitOfWork.Users.Get().Where(el => el.Username == auth.Username).FirstOrDefaultAsync();

            if (objUser == null) { throw new Exception(Messages.UserDontExist); }

            if (objUser.Password != Cryptography.GetSHA256(auth.Password)) { throw new Exception(Messages.ErrorAuthenticate); }

            var user = _mapper.Map<UserDto>(objUser);

            return user;
        }

        public async Task<List<RoleDto>> GetRolesUser(UserDto user)
        {
            var objRoles = await (from a in _unitOfWork.UserRoles.Get()
                           join b in _unitOfWork.Roles.Get() on new { a.RoleId, Active = true } equals new { b.RoleId, Active = (bool)b.Active }
                           where a.UserId == user.UserId &&
                                 a.Active == true

                           select new RoleDto
                           {
                               RoleId = a.RoleId,
                               RoleName = b.RoleName,

                           }).ToListAsync(); ;

            return objRoles;

        }

        #endregion
    }
}
