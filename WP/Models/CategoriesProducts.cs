namespace WP.Models
{
    public class CategoriesProducts
    {
        public IList<Category> categories { get; set; }
        public IList<Product> products { get; set; }
    }
}
