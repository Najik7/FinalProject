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
    public class BrandController:Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> GetAll()
        {
            var brands = await _context.Brands.Select(x => new BrandViewModel(){Id = x.Id,Name = x.Name}).ToListAsync();
            
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var brand = new Brand{Name = model.Name};
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            EditBrandViewModel model = new EditBrandViewModel(){Id = brand.Id, Name = brand.Name};
            
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var brand = await _context.Brands.FindAsync(model.Id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Name = model.Name;

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            //_context.Entry(brand).State = EntityState.Deleted;
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }
        
    }
}