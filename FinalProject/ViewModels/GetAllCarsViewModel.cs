using System.Collections.Generic;

namespace FinalProject.ViewModels
{
    public class GetAllCarsViewModel
    {
        public List<CarViewModel> Cars { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public Dictionary<int,string> FuelTypes { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}