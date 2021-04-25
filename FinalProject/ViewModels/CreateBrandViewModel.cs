using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class CreateBrandViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }
    }
}