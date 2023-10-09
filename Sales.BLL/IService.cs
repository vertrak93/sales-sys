using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL
{
    public interface IService
    {
        public Task Add();
        public Task Delete();

    }
}
