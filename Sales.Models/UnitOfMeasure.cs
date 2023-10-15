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
    public class UnitOfMeasure : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitOfMeasureId { get; set; }

        [Required, StringLength(100)]
        public string UnitOfMeasureName { get; set; }

        [Required, StringLength(10)]
        public string Abbreviation { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
