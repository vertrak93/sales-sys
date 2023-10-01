using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class VendorBankAccountDto
    {
        public int VendorId { get; set; }
        public int BankAccountId { get; set; }
        public string AccountNumber { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
    }
}
