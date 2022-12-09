using System.ComponentModel.DataAnnotations;

namespace WP.Models
{
    public class Comment
    {
        [Key]
        public int commentID { get; set; }
        public string commentString { get; set; }
        public User commentUser { get; set; }
        public DateTime commentDate { get; set; }
    }
}
