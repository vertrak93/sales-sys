using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool? Active { get; set; }
    }
}
