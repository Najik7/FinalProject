using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Context.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class CategoryController:Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _context.Categories.Select(x => new CategoryViewModel(){Id = x.Id,Name = x.Name}).ToListAsync();
            
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = new Category(){Name = model.Name};
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            EditCategoryViewModel model = new EditCategoryViewModel(){Id = category.Id, Name = category.Name};
            
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await _context.Categories.FindAsync(model.Id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            //_context.Entry(brand).State = EntityState.Deleted;
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }
        
        
    }
}