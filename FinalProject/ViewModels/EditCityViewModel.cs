using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class EditCityViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}