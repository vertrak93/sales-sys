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
    public class Vendor : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }

        [Required, MaxLength(50)]
        public string VendorName { get; set; }

        [Required, MaxLength(50)]
        public string TIN { get; set; } //Taxpayer Identification Number

        public ICollection<VendorBankAccount>? VendorBankAccounts { get; set; }
        public ICollection<VendorPhone>? VendorPhones { get; set; }
        public ICollection<VendorAddress>? VendorAddresses { get; set; }
        public ICollection<VendorProduct>? VendorProducts { get; set; }
    }
}
