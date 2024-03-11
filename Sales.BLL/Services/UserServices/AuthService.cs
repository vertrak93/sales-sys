using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.BLL.Services.UserServices;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils;
using Sales.Utils.Constants;
using Sales.Utils.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class AuthService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleService _roleService;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, RoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleService = roleService;
        }
        #endregion

        #region Metodos

        public async Task<TokenDto> Authenticate(AuthenticateDto auth, string keyJwt, double expirationTime, double expirationTimeRT)
        {
            var user = await ValidateLogin(auth);
            var roles = await _roleService.GetRolesByUserId(user.UserId);

            var AccessToken = TokenGenerator.Instance().GenerateJWTToken(user, roles, keyJwt, expirationTime);
            var RefreshToken = await CreateRefreshToken(user, expirationTimeRT);

            await _unitOfWork.SaveAsync();

            return new TokenDto
            {
                Jwt = AccessToken,
                RefreshToken = RefreshToken,
                User = user,
                Role = roles
            };
        }

        public async Task<string> CreateRefreshToken(UserDto user, double expirationTime)
        {
            if(user == null) { throw new Exception(Messages.InvalidUser); };

            await InactiveAllRefresToken(user.UserId);

            DateTime now = DateTime.UtcNow;

            RefreshToken refreshToken = new RefreshToken();
            string token = TokenGenerator.Instance().GenerateRefreshToken();

            refreshToken.UserId = user.UserId;
            refreshToken.Token = token;
            refreshToken.Expiration = now.AddHours(expirationTime);

            await _unitOfWork.RefreshTokens.Add(refreshToken);

            return token;
        }

        public async Task<bool> InactiveAllRefresToken(int UserId)
        {
            var obj = await (from a in _unitOfWork.RefreshTokens.Get()
                             where a.Active == true
                             && a.UserId == UserId
                             select new
                             {
                                 ID = a.RefreshTokenId
                             }).ToListAsync();

            foreach(var item in obj)
            {
                await _unitOfWork.RefreshTokens.Delete(item.ID);
            }

            return true;
        }

        public async Task<UserDto> ValidateLogin(AuthenticateDto auth)
        {
            var objUser = await _unitOfWork.Users.Get().Where(el => el.Username.ToUpper() == auth.Username.Trim().ToUpper() && el.Active == true ).FirstOrDefaultAsync();

            if (objUser == null) { throw new Exception(Messages.UserDontExist); }

            if (objUser.Password != Cryptography.GetSHA256(auth.Password)) { throw new Exception(Messages.ErrorAuthenticate); }

            var user = _mapper.Map<UserDto>(objUser);

            return user;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokens, string keyJwt, double expirationTime, double expirationTimeRT)
        {
            var objToken = await (from a in _unitOfWork.RefreshTokens.Get()
                                  join b in _unitOfWork.Users.Get() on a.UserId equals b.UserId
                                  where a.Token == tokens.RefreshToken && a.Active == true && b.Active == true
                                  select new
                                  {
                                      RefreshToken = a,
                                      User = b,
                                  }).FirstOrDefaultAsync();

            var userName = TokenGenerator.Instance().GetUserFromJwt(tokens.Jwt, keyJwt);

            if (objToken.User.Username != userName)
            {
                throw new Exception(Messages.InvalidUser);
            }

            if (objToken.RefreshToken.Expiration < DateTime.UtcNow)
            {
                throw new Exception(Messages.TokenExpired);
            }

            var roles = await _roleService.GetRolesByUserId(objToken.User.UserId);
            var userDto = _mapper.Map<UserDto>(objToken.User);

            var Jwt = TokenGenerator.Instance().GenerateJWTToken(userDto, roles, keyJwt, expirationTime);
            var RefreshToken = await CreateRefreshToken(userDto, expirationTimeRT);

            await _unitOfWork.SaveAsync();

            return new TokenDto
            {
                RefreshToken = RefreshToken,
                Jwt = Jwt,
            };
        }

        #endregion
    }
}
