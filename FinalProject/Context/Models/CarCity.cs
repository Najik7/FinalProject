using System.Collections.Generic;

namespace FinalProject.Context.Models
{
    public class CarCity
    {
        public int CarId { get; set; }
        public int CityId { get; set; }
        
        public virtual Car Car { get; set; }
        public virtual City City { get; set; }
        
    }
}