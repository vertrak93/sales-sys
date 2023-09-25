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
    public class PurchaseDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseDetailId { get; set; }

        [Required]
        public int VendorProductId { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public double QuantityUnit { get; set; }

        [Required]
        public double PriceUnit { get; set; }

        public double QuantityContainer { get; set; }

        public double PriceContainer { get; set; }

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

        public virtual Purchase Purchase { get; set;}
        public virtual VendorProduct VendorProduct { get; set; }
    }
}
