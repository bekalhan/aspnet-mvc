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
                ListCarts = await _context.ShoppingCarts.Where(m => m.UserId == claim.Value).Include(p => p.User).Include(a => a.Product).ToListAsync(),
                CartTotal = CartTotal()

            };

            var list = await _context.ShoppingCarts.Where(m => m.UserId == claim.Value).ToListAsync();
            var count = list.Count;
            ViewData["count"] = count;

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
            if (cartFromDb == null)
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

            return RedirectToAction("DetailedProducts", "Home", new { id = shoppingCart.ProductId });

        }


        public int CartTotal()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            var shoppingList = _context.ShoppingCarts.Where(m => m.UserId == claim.Value).Include(p => p.User).Include(a => a.Product).ToList();

            int cartTotal = 0;
            foreach (var item in shoppingList)
            {
                cartTotal += item.Count * item.Product.Price;
            };

            return cartTotal;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = _context.ShoppingCarts.Find(id);
            _context.ShoppingCarts.Remove(cart);
            _context.SaveChanges();
            TempData["success"] = "You have been deleted this product succesfully";
            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderProducts()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userID = claim.Value;
            if (userID is not null)
            {
                var list = _context.ShoppingCarts.Where(a => a.UserId == userID).ToList();
                foreach (var item in list)
                {
                    _context.ShoppingCarts.Remove(item);
                }
                _context.SaveChanges();
                TempData["success"] = "You have been ordered succesfuly";
            }
            else
            {
                NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

