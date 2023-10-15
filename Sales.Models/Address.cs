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
    public class Address : BaseEntityModel
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required, StringLength(200)]
        public string AddressDescription { get; set; }

        public ICollection<VendorAddress>? VendorAddresses { get; set; }
    }
}
