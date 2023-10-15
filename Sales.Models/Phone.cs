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
    public class Phone : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneId { get; set; }

        [Required]
        public int TelephonyId { get; set; }

        [Required, StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

        public virtual Telephony Telephony { get; set; }
        public ICollection<VendorPhone>? VendorPhones { get; set; }
    }
}
