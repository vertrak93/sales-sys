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
    public class UserRole : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
