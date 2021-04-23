using System.Threading.Tasks;
using FinalProject.Context.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class AccountController:Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        
        [HttpGet]
        public IActionResult Login(string returnURL)
        {
            return View(new LoginViewModel{ReturnURL = returnURL});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Login,model.Password,false,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnURL ?? "/Home/Index");
            }
            
            ModelState.AddModelError("Login", "Невенрный логин или пароль!");
            
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Login");
        }
        
        
        
    }
}