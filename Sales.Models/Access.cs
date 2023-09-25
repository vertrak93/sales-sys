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
    public class Access
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccessId { get; set; }

        [Required, StringLength(100)]
        public string AccessName { get; set; }

        [Required, StringLength(200)]
        public string? Route { get; set; }

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

        public ICollection<RoleAccess>? RoleAccess { get; set; }
    }
}
