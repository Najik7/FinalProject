using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Context.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class CityController:Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> GetAll()
        {
            var cities = await _context.Brands.Select(x => new CityViewModel(){Id = x.Id,Name = x.Name}).ToListAsync();
            
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var city = new City(){Name = model.Name};
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            EditCityViewModel model = new EditCityViewModel(){Id = city.Id, Name = city.Name};
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var city = await _context.Cities.FindAsync(model.Id);
            if (city == null)
            {
                return NotFound();
            }

            city.Name = model.Name;

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            //_context.Entry(brand).State = EntityState.Deleted;
            _context.Cities.Remove(city);

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }
    }
}