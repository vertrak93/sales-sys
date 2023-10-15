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
    public class VendorProduct : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorProductId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set;}
        public virtual Product Product { get; set;}

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }

    }
}
