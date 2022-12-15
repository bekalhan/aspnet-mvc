using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WP.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WP.Controllers
{
    public class CommentController : Controller
    {
        readonly private WebAppContext _context;

        public HomeController(ILogger<WebAppContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

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
    }
}

