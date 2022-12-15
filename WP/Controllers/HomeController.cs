using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

    /*  public IActionResult CommentSection(int productId, string context)
      {
          Comment comment = new Comment();
          comment.ProductId = productId;
          comment.commentDate = DateTime.Now;
          comment.commentString = context;
          _context.Add(comment);
          _context.SaveChanges();
          return RedirectToAction("Index");
      }*/

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult AddCommentToProduct(Comment comment, string context)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        comment.UserId = claim.Value;

        _context.Comments.Add(comment);
        _context.SaveChanges();

        TempData["success"] = "You has been added a comment for this product";

        return RedirectToAction("Index");

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
            ProductComments = await _context.Comments.Where(a => a.ProductId == id).Include(p => p.User).ToListAsync(),
        };
        cc.ShoppingCart = shoppingobje;
        cc.Comment = commentobje;
        cc.Product = productobje;

        return View(cc);
    }

  /*  [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> DetailedProducts(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.UserId = claim.Value;
        
        _context.ShoppingCarts.Add(shoppingCart);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }*/

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

