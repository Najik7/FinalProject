using System.Collections.Generic;

namespace FinalProject.Context.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<CarCity>CarCities { get; set; }
    }
}