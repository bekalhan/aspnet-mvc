using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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



    public IActionResult Index(string? bname,string? search)
    {
        CategoriesProducts cp = new CategoriesProducts();
        var categoryObject = cp.categories;
        var productObject = cp.products;
        categoryObject = _context.Categories.ToList();
        ViewData["currentSearch"] = search;
        if (search != null)
        {
            if (bname != null)
            {
                if (bname == "Ascending")
                {
                    productObject = _context.Products.Where(x=>x.Title.Contains(search)).OrderBy(y => y.Price).ToList();
                }
                if (bname == "Descending")
                {
                    productObject = _context.Products.Where(x => x.Title.Contains(search)).OrderByDescending(y => y.Price).ToList();
                }
            }
            else
            {
                productObject = _context.Products.Where(x => x.Title.Contains(search)).OrderBy(y=>y.Title).ToList();
            }
        }
        else
        {
            if (bname != null)
            {
                if (bname == "Ascending")
                {
                    productObject = (from y in _context.Products select y).OrderBy(y => y.Price).ToList();
                }
                if (bname == "Descending")
                {
                    productObject = (from y in _context.Products select y).OrderByDescending(y => y.Price).ToList();
                }
            }
            else
            {
                productObject = (from y in _context.Products select y).OrderBy(y => y.Title).ToList();
            }
        }
        
        cp.products = productObject;
        cp.categories = categoryObject;
        return View(cp);
    }
   
    public IActionResult FilteredProducts(int id,string? bname)
    {
        CategoriesProducts cp2 = new CategoriesProducts();
        var categoryObject = cp2.categories;
        var filteredproducts = cp2.products;
        ViewData["categoryid"] = id;
        if (bname != null)
        {
            if (bname == "Ascending")
            {
                filteredproducts = _context.Products.Where(x => x.CategoryId == id).OrderBy(y => y.Price).ToList();
            }
            if (bname == "Descending")
            {
                filteredproducts = _context.Products.Where(x => x.CategoryId == id).OrderByDescending(y => y.Price).ToList();
            }
        }
        else
        {
            filteredproducts = _context.Products.Where(x => x.CategoryId == id).ToList();
        }
        categoryObject = _context.Categories.ToList();
        var name = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
        ViewData["name"] = name.CategoryName;
        cp2.products = filteredproducts;
        cp2.categories = categoryObject;
        return View(cp2);

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

