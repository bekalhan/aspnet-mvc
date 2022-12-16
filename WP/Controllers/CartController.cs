using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WP.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WP.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: /<controller>/
        readonly private WebAppContext _context;
        public CartVM cartVM { get; set; }

        public CartController(WebAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            cartVM = new CartVM()
            {
                ListCarts = await _context.ShoppingCarts.Where(m => m.UserId == claim.Value).Include(p => p.User).ToListAsync(),
                
        };  
            return View(cartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProducttoCart(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.UserId = claim.Value;

            ShoppingCart cartFromDb = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == claim.Value && u.ProductId == shoppingCart.ProductId);
            if(cartFromDb==null)
            {
                _context.ShoppingCarts.Add(shoppingCart);
            }
            else
            {
                cartFromDb.Count += shoppingCart.Count;
                if (cartFromDb.Count < 0)
                {
                    TempData["failed"] = "Count can not be lower than 0";
                    return RedirectToAction("DetailedProducts", "Home", new { id = shoppingCart.ProductId });
                }
                
                _context.Update(cartFromDb);
            }
           

            
            _context.SaveChanges();

            TempData["success"] = "You have added a product to cart";
            
            return RedirectToAction("DetailedProducts", "Home", new {id = shoppingCart.ProductId});

        }
    }
}

