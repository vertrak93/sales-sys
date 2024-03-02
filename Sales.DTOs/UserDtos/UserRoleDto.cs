using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs.UserDtos
{
    public class UserRoleDto
    {
        public int? UserRoleId { get; set; }
        public int? UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
