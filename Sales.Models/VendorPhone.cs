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
    public class VendorPhone : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorPhoneId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int PhoneId { get; set; }

        public virtual Phone Phone { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
