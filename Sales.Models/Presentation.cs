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
    public class Presentation : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PresentationId { get; set; }

        [Required, StringLength(100)]
        public string PresentationName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
