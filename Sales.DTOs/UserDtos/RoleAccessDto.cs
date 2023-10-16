using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs.UserDtos
{
    public class RoleAccessDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int AccessId { get; set; }
        public string AccessName { get; set; }
    }
}
