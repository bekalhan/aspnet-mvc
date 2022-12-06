using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WP.Controllers
{
    public class CategoryController : Controller
    {
        WebAppContext _context = new WebAppContext();



        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Categories;
            return View(await libraryContext.ToListAsync());
        }

        //create  request


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                TempData["error"] = "Please be sure fill all field";
                return RedirectToAction("Create");
            }

        }

        //update request
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Category updated succesfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");

            }
            return View(category);
        }

        // delete request

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }



            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var book = await _context.Categories.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Remove(book);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}

