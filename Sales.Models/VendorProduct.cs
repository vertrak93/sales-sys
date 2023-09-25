using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Models
{
    public class VendorProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorProductId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        #region Modify Control

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        [Required, StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        #endregion

        public virtual Vendor Vendor { get; set;}
        public virtual Product Product { get; set;}


    }
}
