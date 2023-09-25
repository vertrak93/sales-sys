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
    public class VendorPhone
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorPhoneId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int PhoneId { get; set; }

        #region Modify Control

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        [Required, StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        #endregion

        public virtual Phone Phone { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
