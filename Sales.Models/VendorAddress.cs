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
    public class VendorAddress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorAddressId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        #region Modify Control
        [Required, StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        #endregion

        public virtual Vendor Vendor { get; set; }
        public virtual Address Address { get; set; }
    }
}
