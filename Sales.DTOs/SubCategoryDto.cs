﻿namespace Sales.DTOs
{
    public class SubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string? CategoryName { get; set; }
    }
}
