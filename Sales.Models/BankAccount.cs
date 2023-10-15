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
    public class BankAccount : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccoutId { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required, StringLength(100)]
        public string AccountNumber { get; set; }

        public virtual Bank Bank { get; set; }
        public ICollection<VendorBankAccount>? VendorBankAccounts { get; set; }
    }
}
