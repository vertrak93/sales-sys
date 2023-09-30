using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int PresentationId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal MinimumStock { get; set; }
        public bool? IsContainer { get; set; }

    }
}
