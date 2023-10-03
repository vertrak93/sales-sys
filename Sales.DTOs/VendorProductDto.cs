using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class VendorProductDto
    {
        public int VendorProductId { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
    }
}
