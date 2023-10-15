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
    public class RoleAccess : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleAccessId { get; set; }

        [Required]
        public int AccessId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public virtual Access Access { get; set; }

        public virtual Role Role { get; set; }
    }
}
