using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public abstract class UserInfo
    {
        private string _UserLogged = string.Empty;
        public string UserLogged { get { return _UserLogged; } set { _UserLogged = value; } }
    }
}
