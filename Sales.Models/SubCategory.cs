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
    public class SubCategory : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string NameSubCatagory { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
