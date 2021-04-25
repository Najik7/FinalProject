using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}