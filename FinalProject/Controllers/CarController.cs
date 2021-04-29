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
                Model = model.ModelName,
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

            return RedirectToAction("GetAll");
        }

        
        
        public async Task<IActionResult> GetAll()
        {
            var cars = await _context.Cars
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.BodyType)
                .Include(x => x.CarCities).ThenInclude(x => x.City)
                .Include(x => x.CarFuelTypes).ThenInclude(x => x.FuelType)
                .Select(x => new CarViewModel()
                {
                    Id = x.Id,
                    Doors = x.Doors,
                    Model = x.Model,
                    Passengers = x.Passengers,
                    BaggageCount = x.BaggageCount,
                    BrandName = x.Brand.Name,
                    CanDeparture = x.CanDeparture,
                    CategoryName = x.Category.Name,
                    DailyPrice = x.DailyPrice,
                    EngineVolume = x.EngineVolume,
                    FromAge = x.FromAge,
                    HasConditioning = x.HasConditioning,
                    ImagePath = x.ImagePath,
                    IsFree = x.IsFree,
                    IsManual = x.IsManual,
                    RequiredMileage = x.RequiredMileage,
                    TilAge = x.TilAge,
                    BodyTypeName = x.BodyType.Name,
                    Cities = x.CarCities.Select(c=>c.City.Name).ToList(),
                    FuelTypes = x.CarFuelTypes.Select(f=>f.FuelType.Name).ToList()
                }).ToListAsync();
            return View(cars);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            var carFuelTypes = await _context.CarFuelTypes.Where(x => x.CarId == id).ToListAsync();

            var carCities = await _context.CarCities.Where(x => x.CarId == id).ToListAsync();
            
            _context.CarFuelTypes.RemoveRange(carFuelTypes);
            _context.CarCities.RemoveRange(carCities);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _context.Cars.FindAsync(id);


            var model = new EditCarViewModel()
            {
                BrandId = car.BrandId,
                CategoryId = car.CategoryId,
                Doors = car.Doors,
                ModelName = car.Model,
                Passengers = car.Passengers,
                BaggageCount = car.BaggageCount,
                CanDeparture = car.CanDeparture,
                DailyPrice = car.DailyPrice,
                FromAge = car.FromAge,
                EngineVolume = car.EngineVolume,
                HasConditioning = car.HasConditioning,
                IsFree = car.IsFree,
                IsManual = car.IsManual,
                TilAge = car.TilAge,
                RequiredMileage = car.RequiredMileage,
                BodyTypeId = car.BodyTypeId,
                Brands = await _context.Brands.Select(x => new BrandViewModel {Id = x.Id, Name = x.Name}).ToListAsync(),
                FuelTypes = await _context.FuelTypes.ToDictionaryAsync(x => x.Id, x => x.Name),
                Categories = await _context.Categories.Select(x => new CategoryViewModel {Id = x.Id, Name = x.Name})
                    .ToListAsync(),
                Cities = await _context.Cities.Select(x => new CityViewModel {Id = x.Id, Name = x.Name}).ToListAsync(),
                BodyTypes = await _context.BodyTypes.ToDictionaryAsync(x=>x.Id, x=>x.Name),
                CitiesId = await _context.CarCities.Where(x=>x.CarId == car.Id).Select(x=>x.CityId).ToListAsync(),
                FuelTypesId = await _context.CarFuelTypes.Where(x=>x.CarId == car.Id).Select(x=>x.FuelTypeId).ToListAsync(),
                Id = id
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel model)
        {
            var car = await _context.Cars.FindAsync(model.Id);

            car.BrandId = model.BrandId;
            car.CategoryId = model.CategoryId;
            car.Doors = model.Doors;
            car.Model = model.ModelName;
            car.Passengers = model.Passengers;
            car.BaggageCount = model.BaggageCount;
            car.CanDeparture = model.CanDeparture;
            car.DailyPrice = model.DailyPrice;
            car.FromAge = model.FromAge;
            car.EngineVolume = model.EngineVolume;
            car.HasConditioning = model.HasConditioning;
            car.IsFree = model.IsFree;
            car.IsManual = model.IsManual;
            car.TilAge = model.TilAge;
            car.RequiredMileage = model.RequiredMileage;
            car.BodyTypeId = model.BodyTypeId;
            car.ImagePath = model.Image != null ? $"/images/{model.Image.FileName}" : car.ImagePath;

            #region adding file
            if (model.Image != null)
            {
                string dirPath = _environment.WebRootPath + "/images";
               
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                
                if (System.IO.File.Exists(dirPath+$"/{model.Image.FileName}"))
                {
                    System.IO.File.Delete(dirPath+$"/{model.Image.FileName}");
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

            var carCities = await _context.CarCities.Where(x => x.CarId == car.Id).ToListAsync();
            var carFuelTypes = await _context.CarFuelTypes.Where(x => x.CarId == car.Id).ToListAsync();

            _context.CarCities.RemoveRange(carCities);
            _context.CarFuelTypes.RemoveRange(carFuelTypes);

            foreach (var i in model.CitiesId)
            {
                await _context.CarCities.AddAsync(new CarCity {CarId = car.Id, CityId = i});
            }
            
            foreach (var i in model.FuelTypesId)
            {
                await _context.CarFuelTypes.AddAsync(new CarFuelType {CarId = car.Id, FuelTypeId = i});
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetAll");
        }
        
        
    }
}