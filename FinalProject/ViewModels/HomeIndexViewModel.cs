using System.Collections.Generic;

namespace FinalProject.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<CityViewModel> Cities { get; set; }
        public Dictionary<int,string> FuelTypes { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}