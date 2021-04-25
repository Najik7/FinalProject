using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class CreateCityViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}