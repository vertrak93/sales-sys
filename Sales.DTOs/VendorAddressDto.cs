using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class VendorAddressDto
    {
        public int VendorId { get; set; }
        public int VendorAddressId { get; set; }
        public int AddressId { get; set; }
        public string AddressDescription { get; set; }
    }
}
