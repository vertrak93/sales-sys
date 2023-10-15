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
    public class VendorBankAccount : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorBankAccountId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int BankAccountId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
