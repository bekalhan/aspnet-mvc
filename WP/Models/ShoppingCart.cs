using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WP.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        public int Count { get; set; }
        public string UserId { get; set; }

        [ValidateNever]
        [ForeignKey("UserId")]
        public User User { get; set; }  

    }
}
