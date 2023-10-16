using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils.Constants
{
    public class RegularExpressions
    {
        #region User
        public static readonly string UserNameRegx = "^[a-zA-Z0-9](_(?!(\\.|_))|\\.(?!(_|\\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$";
        public static readonly string PasswordRegx = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
        public static readonly string EmailRegx = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        #endregion
    }
}
