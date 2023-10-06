using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils
{
    public class Disposable : IDisposable
    {
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
