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
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        #region Modify Control
        [Required, StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        #endregion

        public ICollection<UserRole>? UserRol { get; set; }

        public ICollection<RoleAccess>? RoleAccess { get; set; }
    }
}
