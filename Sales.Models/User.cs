using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class User : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string FisrtName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(500)]
        public string Password { get; set; }

        public ICollection<UserRole>? UserRol { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
