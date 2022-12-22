using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WP.Models;

namespace WP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    readonly private WebAppContext _context;

    public HomeController(ILogger<HomeController> logger, WebAppContext context)
    {
        _logger = logger;
        _context = context;
    }



    public IActionResult Index()
    {
        var allProducts = (from y in _context.Products select y).ToList();
        return View(allProducts);
    }
    public IActionResult ChangeLanguage(string culture)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return Redirect(Request.Headers["Referer"].ToString());
    }
    public async Task<IActionResult> DetailedProducts(int id)
    {
        CartCommentProduct cc = new CartCommentProduct();
        var shoppingobje = cc.ShoppingCart;
        var commentobje = cc.Comment;
        var productobje = cc.Product;

        shoppingobje = new()
        {
            ProductId = id,
            Product = await _context.Products.Include(k => k.Category).FirstOrDefaultAsync(m => m.ID == id),
        };
        commentobje = new()
        {
            ProductId = id,
        };
        productobje = new()
        {
               ProductComments = await _context.Comments.Where(a => a.ProductId == id).Include(p => p.User)?.ToListAsync(),    
        };
        cc.ShoppingCart = shoppingobje;
        cc.Comment = commentobje;
        cc.Product = productobje;

        return View(cc);
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

