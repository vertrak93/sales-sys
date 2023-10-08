using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs.UtilsDto
{
    public class AppSettingsDto
    {
        public string KeyJwt { get; set; }
        public int ExpirationTimeJwt { get; set; }
        public int ExpirationRefreshToken { get; set; }
    }
}
