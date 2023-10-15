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
    public class Purchase : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int PurchaseTypeId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public DateTime PaymentDeadline { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual PurchaseType PurchaseType { get; set; }
        public virtual Invoice Invoice { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
