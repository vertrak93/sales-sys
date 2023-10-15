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
    public class VendorAddress : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorAddressId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int AddressId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual Address Address { get; set; }
    }
}
