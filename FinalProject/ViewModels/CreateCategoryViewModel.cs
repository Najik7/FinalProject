using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}