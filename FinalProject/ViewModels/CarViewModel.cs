namespace FinalProject.ViewModels
{
    public class CarViewModel
    {
        public string Model { get; set; }
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
        public string ImagePath { get; set; }
        
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string BodyTypeName { get; set; }
        
    }
}