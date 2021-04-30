using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}