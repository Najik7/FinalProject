using System;
using System.Collections.Generic;

namespace FinalProject.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TilDate { get; set; }
        public bool HasAdditionalDriver { get; set; }
        public bool HasKidChair { get; set; }
        public bool HasInsurance { get; set; }
        public string Status { get; set; }
        public double Summ { get; set; }
        public string Model { get; set; }
        public int Doors { get; set; }
        public int Passengers { get; set; }
        public int BaggageCount { get; set; }
        public double EngineVolume { get; set; }
        public bool HasConditioning { get; set; }
        public bool IsManual { get; set; }
        public bool CanDeparture { get; set; }
        public bool RequiredMileage { get; set; }
        public int FromAge { get; set; }
        public int TilAge { get; set; }
        public string ImagePath { get; set; }
        public List<string> FuelTypes { get; set; }
        public List<string> Cities { get; set; }
        
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string BodyTypeName { get; set; }
    }
}