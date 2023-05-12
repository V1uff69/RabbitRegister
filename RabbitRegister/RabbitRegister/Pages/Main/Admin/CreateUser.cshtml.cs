using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.UserService;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RabbitRegister.Pages.Main.Admin
{
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel

    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private UserService _userService;

        private PasswordHasher<string> passwordHasher;

        public CreateUserModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _userService.AddUserAsync(new User(UserName, passwordHasher.HashPassword(null, Password)));
            return RedirectToPage("/Index");
        }
    }
}
