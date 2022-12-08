using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BackUp.Models
{
	public class Category
	{
        public Category()
        {

        }
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "İsminiz 20 karakterden küçük olmalıdır."), MinLength(3, ErrorMessage = "İsminiz 3 karakterden büyük olmalıdır.")]
        public string CategoryName { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}

