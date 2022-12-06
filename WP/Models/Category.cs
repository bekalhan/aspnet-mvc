using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WP.Models
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

