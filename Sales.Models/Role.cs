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
    public class Role : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        public ICollection<UserRole>? UserRol { get; set; }

        public ICollection<RoleAccess>? RoleAccess { get; set; }
    }
}
