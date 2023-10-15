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
    public class Access: BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccessId { get; set; }

        [Required, StringLength(100)]
        public string AccessName { get; set; }

        [Required, StringLength(200)]
        public string? Route { get; set; }

        public ICollection<RoleAccess>? RoleAccess { get; set; }
    }
}
