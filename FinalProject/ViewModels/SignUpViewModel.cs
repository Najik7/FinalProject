using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}