using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WP.Models;

namespace WP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    readonly private WebAppContext _context;

    public HomeController(ILogger<HomeController> logger,WebAppContext context)
    {
        _logger = logger;
        _context = context;
    }
    

   
    public IActionResult Index()
    {
        var allProducts = (from y in _context.Products select y).ToList();
        return View(allProducts);
    }
    public IActionResult CommentSection(int productID,string context)
    {
        Comment comment = new Comment(); 
        comment.product.ID = productID;
        comment.commentDate = DateTime.Now;
        comment.commentString = context;
        _context.Add(comment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> DetailedProducts(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var choosenProduct = await _context.Products.Include(k => k.Category).FirstOrDefaultAsync(m => m.ID == id);
        
        if (choosenProduct == null)
        {
            return NotFound();
        }

        return View(choosenProduct);
    }
   
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

