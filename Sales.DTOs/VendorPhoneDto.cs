using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class VendorPhoneDto
    {
        public int VendorId { get; set; }
        public int VendorPhoneId { get; set; }
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string? Comment { get; set; }
        public int TelephonyId { get; set; }
        public string TelephonyName { get; set; }
    }
}
