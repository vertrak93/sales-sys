using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils.Jwt.Interfaces
{
    public interface ICurrentUser
    {
        string GetUserId();
        string GetUserMail();
    }
}
