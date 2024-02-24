using Microsoft.AspNetCore.Http;
using Sales.Utils.Jwt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils.Jwt
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;
        public CurrentUser(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            try
            {
                return _context.HttpContext.User.Claims
                           .First(i => i.Type == ClaimTypes.Name).Value;
            }
            catch { return string.Empty; }
        }

        public string GetUserMail()
        {
            return _context.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.Email).Value;
        }
    }
}
