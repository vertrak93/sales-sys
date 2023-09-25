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
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int PresentationId { get; set; }

        [Required, StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string SKU { get; set; }

        [Required]
        public decimal MinumunStock { get; set; }

        [DefaultValue(false)]
        public bool? IsContainer { get; set; }

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

        public virtual Category Category { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Presentation Presentation { get; set; }
        public ICollection<VendorProduct>? VendorProducts { get; set; }
    }
}
