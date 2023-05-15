using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Model;

namespace RabbitRegister.Pages.Main.LogIn
{
    public class LogInPageModel : PageModel
    {

        private IBreederService _breederService { get; set; }

        [BindProperty]
        public int BreederRegNo { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public LogInPageModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            List<Model.Breeder> breeders = _breederService.Breeders;
            foreach (Model.Breeder breeder in breeders)
            {
                if (BreederRegNo == breeder.BreederRegNo)
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, breeder.Password, Password) == PasswordVerificationResult.Success)
                    {
                        string UserName = BreederRegNo.ToString();
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };

                        if (breeder.isAdmin == true) claims.Add(new Claim(ClaimTypes.Role, "admin"));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Index");
                    }
                }
            }
            Message = "Invalid attempt";
            return Page();

            //List<User> users = _userService.Users;
            //foreach (User user in users)
            //{
            //    if (UserName == user.UserName)
            //    {
            //        var passwordHasher = new PasswordHasher<string>();

            //        if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
            //        {
            //            // LoggedInUser = user;

            //            var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };

            //            if (UserName == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

            //            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            //            return RedirectToPage("/Index");
            //        }
            //    }
            //}
            //Message = "Invalid attempt";
            //return Page();

        }

    }
}
