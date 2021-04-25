using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Context.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class CarController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CarController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateCarViewModel
            {
                Brands = await _context.Brands.Select(x => new BrandViewModel {Id = x.Id, Name = x.Name}).ToListAsync(),
                FuelTypes = await _context.FuelTypes.ToDictionaryAsync(x => x.Id, x => x.Name),
                Categories = await _context.Categories.Select(x => new CategoryViewModel {Id = x.Id, Name = x.Name})
                    .ToListAsync(),
                Cities = await _context.Cities.Select(x => new CityViewModel {Id = x.Id, Name = x.Name}).ToListAsync(),
                BodyTypes = await _context.BodyTypes.ToDictionaryAsync(x=>x.Id, x=>x.Name)
            };
            
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Brands = await _context.Brands.Select(x => new BrandViewModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
                model.FuelTypes = await _context.FuelTypes.ToDictionaryAsync(x => x.Id, x => x.Name);
                model.Categories = await _context.Categories
                    .Select(x => new CategoryViewModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
                model.Cities = await _context.Cities.Select(x => new CityViewModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
                model.BodyTypes = await _context.BodyTypes.ToDictionaryAsync(x => x.Id, x => x.Name);
                return View(model);
            }

            #region adding file
            if (model.Image != null)
            {
                string dirPath = _environment.WebRootPath + "/images";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                string imagePath = dirPath + $"/{model.Image.FileName}";

                if (!System.IO.File.Exists(imagePath))
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fs);
                    }
                }
            }
            #endregion

            var car = new Car
            {
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Doors = model.Doors,
                Model = model.Model,
                Passengers = model.Passengers,
                BaggageCount = model.BaggageCount,
                CanDeparture = model.CanDeparture,
                DailyPrice = model.DailyPrice,
                FromAge = model.FromAge,
                ImagePath = model.Image != null ? $"/images/{model.Image.FileName}" : null,
                EngineVolume = model.EngineVolume,
                HasConditioning = model.HasConditioning,
                IsFree = model.IsFree,
                IsManual = model.IsManual,
                TilAge = model.TilAge,
                RequiredMileage = model.RequiredMileage,
                BodyTypeId = model.BodyTypeId
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            foreach (var i in model.FuelTypesId)
            {
                await _context.CarFuelTypes.AddAsync(new CarFuelType {CarId = car.Id, FuelTypeId = i});
            }

            foreach (var i in model.CitiesId)
            {
                await _context.CarCities.AddAsync(new CarCity {CarId = car.Id, CityId = i});
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        }

        
        
        public async Task<IActionResult> GetAll()
        {
            var cars = await (from p in _context.Cars
                join c in _context.Categories on p.CategoryId equals c.Id
                join b in _context.Brands on p.BrandId equals b.Id
                join t in _context.BodyTypes on p.BodyTypeId equals t.Id
                select new CarViewModel()
                {
                    Model = p.Model,
                    Doors = p.Doors,
                    Passengers = p.Passengers,
                    BaggageCount = p.BaggageCount,
                    EngineVolume = p.EngineVolume,
                    HasConditioning = p.HasConditioning,
                    IsManual = p.IsManual,
                    CanDeparture = p.CanDeparture,
                    RequiredMileage = p.RequiredMileage,
                    DailyPrice = p.DailyPrice,
                    IsFree = p.IsFree,
                    FromAge = p.FromAge,
                    TilAge = p.TilAge,
                    ImagePath = p.ImagePath,
                    
                    CategoryName = c.Name,
                    BrandName = b.Name,
                    BodyTypeName = t.Name
                    
                    
                }).ToListAsync();
        }
    }
}