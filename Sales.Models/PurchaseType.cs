using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class PurchaseType : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseTypeId { get; set; }

        [Required, MaxLength(100)]
        public string PurchaseTypeName { get; set; }

        public ICollection<Purchase>? Purchases { get; set; }
    }
}
