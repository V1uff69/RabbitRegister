using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using System.Drawing;
using System.Collections.Generic;


namespace RabbitRegister.Pages.Main.Rabbit
{
    public class CreateRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;


        public CreateRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _rabbitService.AddRabbitAsync(Rabbit);
            return RedirectToPage("GetAllRabbits");
        }
    }
}
