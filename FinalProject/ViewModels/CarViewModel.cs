using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Doors { get; set; }
        [Required]
        public int Passengers { get; set; }
        [Required]
        public int BaggageCount { get; set; }
        [Required]
        public double EngineVolume { get; set; }
        public bool HasConditioning { get; set; }
        public bool IsManual { get; set; }
        public bool CanDeparture { get; set; }
        public bool RequiredMileage { get; set; }
        [Required]
        public double DailyPrice { get; set; }
        public bool IsFree { get; set; }
        [Required]
        public int FromAge { get; set; }
        [Required]
        public int TilAge { get; set; }
        public string ImagePath { get; set; }
        
        public List<string> FuelTypes { get; set; }
        public List<string> Cities { get; set; }
        
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string BodyTypeName { get; set; }
        
    }
}