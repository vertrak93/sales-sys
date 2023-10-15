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
    public class Bank : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankId { get; set; }

        [Required, StringLength(100)]
        public string BankName { get; set; }

        public ICollection<BankAccount>? BankAccounts { get; set; }
    }
}
