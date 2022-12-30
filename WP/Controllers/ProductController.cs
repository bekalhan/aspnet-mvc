using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WP.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        
        readonly private WebAppContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IWebHostEnvironment hostEnvironment, WebAppContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: /<controller>/
        
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Products.Include(k => k.Category);
            return View(await libraryContext.ToListAsync());
        }

        //create  request


        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images/");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    product.Image = @"\Images\" + fileName + extension;
                }
                _context.Add(product);
                _context.SaveChanges();
                TempData["success"] = "Product Created succesfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["error"] = "Please be sure fill all field";
                return RedirectToAction("Create");
            }

        }
        
        //Edit request

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Product product, IFormFile file)
        {
            if (id != product.ID)
            {
                return NotFound();
            }
            if (true)
            {

                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images/");
                        var extension = Path.GetExtension(file.FileName);
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        product.Image = @"\Images\" + fileName + extension;
                    }
                    _context.Update(product);
                    _context.SaveChanges();
                    TempData["success"] = "Product updated succesfully";

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);

        }

        //delete request

        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products.Include(k => k.Category).FirstOrDefault(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirm(int? id)
        {
            var product =  _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            _context.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
    
}

