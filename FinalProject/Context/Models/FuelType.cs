using System.Collections.Generic;

namespace FinalProject.Context.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<CarFuelType> CarFuelTypes { get; set; }
    }
}