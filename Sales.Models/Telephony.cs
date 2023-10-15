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
    public class Telephony : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TelephonyId { get; set; }

        [Required, StringLength(100)]
        public string TelephonyName { get; set; }

        public ICollection<Phone>? Phones { get; set; }
    }
}
