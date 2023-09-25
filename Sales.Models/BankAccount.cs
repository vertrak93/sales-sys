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
    public class BankAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccoutId { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required, StringLength(100)]
        public string AccountNumber { get; set; }

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

        public virtual Bank Bank { get; set; }
        public ICollection<VendorBankAccount>? VendorBankAccounts { get; set; }
    }
}
