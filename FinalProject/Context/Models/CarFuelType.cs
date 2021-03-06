using System.Collections.Generic;

namespace FinalProject.Context.Models
{
    public class CarFuelType
    {
        public int CarId { get; set; }
        public int FuelTypeId { get; set; }
        
        public virtual Car Car { get; set; }
        public virtual FuelType FuelType { get; set; }
        
    }
}