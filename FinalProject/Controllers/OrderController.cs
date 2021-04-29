using System;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Context.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Create(int carId)
        {
            var model = new CreateOrderViewModel
            {
                CarId = carId,
                FromDate = DateTime.Now,
                TilDate = DateTime.Now.AddDays(1)
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            var car = await _context.Cars.FindAsync(model.CarId);
            var currentUser = await _userManager.GetUserAsync(User);
            var order = new Order
            {
                Status = "В ожидании",
                UserId = currentUser.Id,
                CarId = car.Id,
                FromDate = model.FromDate,
                OrderDate = DateTime.Now,
                TilDate = model.TilDate,
                HasInsurance = model.HasInsurance,
                HasAdditionalDriver = model.HasAdditionalDriver,
                HasKidChair = model.HasKidChair
            };

            TimeSpan timeSpan = model.TilDate - model.FromDate;

            int days = (int)timeSpan.TotalMinutes / 1440;

            int minutes = (int) timeSpan.TotalMinutes % 1440;

            days += minutes > 0 ? 1 : 0;

            var summ = car.DailyPrice * days;

            summ += model.HasInsurance ? 500 : 0;
            summ += model.HasAdditionalDriver ? 100 : 0;
            summ += model.HasKidChair ? 50 : 0;

            order.Summ = summ;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAll");
        }

        [Authorize]
        public async Task<IActionResult> Cancel(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.Status = "Отменен";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllForAdmin");
        }
        
        [Authorize]
        public async Task<IActionResult> Approved(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.Status = "Одобрено";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllForAdmin");
        }

        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var orders = await _context.Orders.Where(x=>x.UserId == currentUser.Id).Include(x => x.Car).ThenInclude(x => x.Brand)
                .Include(x => x.Car).ThenInclude(x => x.Category)
                .Include(x => x.Car).ThenInclude(x => x.CarCities)
                .Include(x => x.Car).ThenInclude(x => x.CarFuelTypes)
                .Include(x => x.Car).ThenInclude(x => x.BodyType)
                .Select(x => new OrderViewModel
                {
                    Cities = x.Car.CarCities.Select(c => c.City.Name).ToList(),
                    Doors = x.Car.Doors,
                    FuelTypes = x.Car.CarFuelTypes.Select(f => f.FuelType.Name).ToList(),
                    Id = x.Id,
                    Model = x.Car.Model,
                    Passengers = x.Car.Passengers,
                    Status = x.Status,
                    Summ = x.Summ,
                    BaggageCount = x.Car.BaggageCount,
                    BrandName = x.Car.Brand.Name,
                    CanDeparture = x.Car.CanDeparture,
                    CategoryName = x.Car.Category.Name,
                    BodyTypeName = x.Car.BodyType.Name,
                    EngineVolume = x.Car.EngineVolume,
                    FromAge = x.Car.FromAge,
                    FromDate = x.FromDate,
                    HasConditioning = x.Car.HasConditioning,
                    HasInsurance = x.HasInsurance,
                    ImagePath = x.Car.ImagePath,
                    IsManual = x.Car.IsManual,
                    OrderDate = x.OrderDate,
                    RequiredMileage = x.Car.RequiredMileage,
                    TilAge = x.Car.TilAge,
                    TilDate = x.TilDate,
                    HasAdditionalDriver = x.HasAdditionalDriver,
                    HasKidChair = x.HasKidChair
                }).ToListAsync();
            return View(orders);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var orders = await _context.Orders.Include(x => x.Car).ThenInclude(x => x.Brand)
                .Include(x => x.Car).ThenInclude(x => x.Category)
                .Include(x => x.Car).ThenInclude(x => x.CarCities)
                .Include(x => x.Car).ThenInclude(x => x.CarFuelTypes)
                .Include(x => x.Car).ThenInclude(x => x.BodyType)
                .Select(x => new OrderViewModel
                {
                    Cities = x.Car.CarCities.Select(c => c.City.Name).ToList(),
                    Doors = x.Car.Doors,
                    FuelTypes = x.Car.CarFuelTypes.Select(f => f.FuelType.Name).ToList(),
                    Id = x.Id,
                    Model = x.Car.Model,
                    Passengers = x.Car.Passengers,
                    Status = x.Status,
                    Summ = x.Summ,
                    BaggageCount = x.Car.BaggageCount,
                    BrandName = x.Car.Brand.Name,
                    CanDeparture = x.Car.CanDeparture,
                    CategoryName = x.Car.Category.Name,
                    BodyTypeName = x.Car.BodyType.Name,
                    EngineVolume = x.Car.EngineVolume,
                    FromAge = x.Car.FromAge,
                    FromDate = x.FromDate,
                    HasConditioning = x.Car.HasConditioning,
                    HasInsurance = x.HasInsurance,
                    ImagePath = x.Car.ImagePath,
                    IsManual = x.Car.IsManual,
                    OrderDate = x.OrderDate,
                    RequiredMileage = x.Car.RequiredMileage,
                    TilAge = x.Car.TilAge,
                    TilDate = x.TilDate,
                    HasAdditionalDriver = x.HasAdditionalDriver,
                    HasKidChair = x.HasKidChair
                }).ToListAsync();
            return View(orders);
        }
    }
}