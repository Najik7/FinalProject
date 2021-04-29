using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FinalProject.ViewModels
{
    public class EditCarViewModel 
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int Doors { get; set; }
        public int Passengers { get; set; }
        public int BaggageCount { get; set; }
        public double EngineVolume { get; set; }
        public bool HasConditioning { get; set; }
        public bool IsManual { get; set; }
        public bool CanDeparture { get; set; }
        public bool RequiredMileage { get; set; }
        public double DailyPrice { get; set; }
        public bool IsFree { get; set; }
        public int FromAge { get; set; }
        public int TilAge { get; set; }
        public IFormFile Image { get; set; }

        public List<int> FuelTypesId { get; set; }
        public Dictionary<int,string> FuelTypes { get; set; }
        public List<int> CitiesId { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public int BrandId { get; set; }
        public List<BrandViewModel> Brands { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public int BodyTypeId { get; set; }
        public Dictionary<int,string> BodyTypes { get; set; }
    }
}