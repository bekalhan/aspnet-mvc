using System;
namespace WP.Models
{
	public class CartCommentProduct
	{
        public ShoppingCart ShoppingCart { get; set; }

        public Comment Comment { get; set; }

        public Product Product { get; set; }
    }
}

