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

namespace Sales.BLL.Services
{
    public class AuthService
    {
        #region Declarations
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Metodos

        public async Task<TokenDto> Authenticate(AuthenticateDto auth, string keyJwt, double expirationTime, double expirationTimeRT)
        {
            var user = await ValidateLogin(auth);
            var roles = await GetRolesUser(user.Username);

            var AccessToken = TokenGenerator.Instance().GenerateJWTToken(user, roles, keyJwt, expirationTime);
            var RefreshToken = await CreateRefreshToken(user.Username, expirationTimeRT);

            await _unitOfWork.SaveAsync();

            return new TokenDto
            {
                Jwt = AccessToken,
                RefreshToken = RefreshToken,
                User = user,
                Role = roles
            };
        }

        public async Task<string> CreateRefreshToken(string userName, double expirationTime)
        {
            var user = await _unitOfWork.Users.Get().Where(obj => obj.Username == userName).FirstOrDefaultAsync();
            DateTime now = DateTime.UtcNow;

            RefreshToken refreshToken = new RefreshToken();
            string token = TokenGenerator.Instance().GenerateRefreshToken();

            refreshToken.UserId = user.UserId;
            refreshToken.Token = token;
            refreshToken.Expiration = now.AddHours(expirationTime);
            refreshToken.Active = true;

            await _unitOfWork.RefreshTokens.Add(refreshToken);

            return token;
        }

        public async Task<UserDto> ValidateLogin(AuthenticateDto auth)
        {
            var objUser = await _unitOfWork.Users.Get().Where(el => el.Username.ToUpper() == auth.Username.Trim().ToUpper()).FirstOrDefaultAsync();

            if (objUser == null) { throw new Exception(Messages.UserDontExist); }

            if (objUser.Password != Cryptography.GetSHA256(auth.Password)) { throw new Exception(Messages.ErrorAuthenticate); }

            var user = _mapper.Map<UserDto>(objUser);

            return user;
        }

        public async Task<List<RoleDto>> GetRolesUser(string user)
        {
            var objRoles = await (from a in _unitOfWork.UserRoles.Get()
                                  join b in _unitOfWork.Roles.Get() on new { a.RoleId, Active = true } equals new { b.RoleId, Active = (bool)b.Active }
                                  join c in _unitOfWork.Users.Get() on a.UserId equals c.UserId
                                  where c.Username.ToUpper() == user.Trim().ToUpper() &&
                                        a.Active == true
                                        && c.Active == true
                                  select new RoleDto
                                  {
                                      RoleId = a.RoleId,
                                      RoleName = b.RoleName,

                                  }).ToListAsync(); ;

            return objRoles;

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

            if (objToken.User.Username != _unitOfWork.UserName)
            {
                throw new Exception(Messages.InvalidUser);
            }

            if (objToken.RefreshToken.Expiration < DateTime.UtcNow)
            {
                throw new Exception(Messages.TokenExpired);
            }

            await _unitOfWork.RefreshTokens.Delete(objToken.RefreshToken.RefreshTokenId);

            var roles = await GetRolesUser(objToken.User.Username);
            var userDto = _mapper.Map<UserDto>(objToken.User);

            var Jwt = TokenGenerator.Instance().GenerateJWTToken(userDto, roles, keyJwt, expirationTime);
            var RefreshToken = await CreateRefreshToken(objToken.User.Username, expirationTimeRT);

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
