using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class TokenDto
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
        public List<RoleDto>? Role { get; set; }
    }
}
