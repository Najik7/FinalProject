using System.Collections.Generic;

namespace FinalProject.Context.Models
{
    public class BodyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Car>Cars { get; set; }
    }
}