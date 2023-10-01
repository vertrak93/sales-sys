using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class VendorDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string TIN { get; set; }
        public List<VendorAddressDto> VendorAddress { get; set; }
        public List<VendorBankAccountDto> VendorBankAccount { get; set; }
        public List<VendorPhoneDto> VendorPhone { get; set; }
    }
}
