namespace WP.Models
{
    public class CartVM
    {
        public IEnumerable<ShoppingCart> ListCarts { get; set; }

        public double CartTotal { get; set; }
    }
}
