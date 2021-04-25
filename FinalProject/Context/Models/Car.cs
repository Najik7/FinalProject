namespace FinalProject.Context.Models
{
    public class Car
    {
        public int Id { get; set; }
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

        public int CityId { get; set; }
        public int BodyTypeId { get; set; }
    }
}