using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils.UtilsDto
{
    public class PaginationParams
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        [BindNever]
        public int Start { get { return ((Page - 1) * PageSize); } }
    }
}
