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
    public class Purchase
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

        public virtual Vendor Vendor { get; set; }
        public virtual PurchaseType PurchaseType { get; set; }
        public virtual Invoice Invoice { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
