using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WP.Models
{
    public class Comment
    {
        [Key]
        public int commentID { get; set; }
        public string commentString { get; set; }
        
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product product { get; set; }

        public DateTime commentDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }

        [ValidateNever]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
