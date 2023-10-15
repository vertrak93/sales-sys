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
    public class Category : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; }

        public ICollection<SubCategory>? SubCategories { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
