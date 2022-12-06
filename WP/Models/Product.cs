using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WP.Models
{
	public class Product
	{
        public int ID { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public string Image { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }
    }
}

