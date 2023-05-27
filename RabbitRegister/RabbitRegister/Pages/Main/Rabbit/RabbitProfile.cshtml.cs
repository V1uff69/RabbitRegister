using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    public class RabbitProfileModel : PageModel
    {
        private IRabbitService _rabbitService;

        public RabbitProfileModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet(int id, int breederRegNo)
        {
            Rabbit = _rabbitService.GetRabbit(id, breederRegNo);
            //if (Rabbit == null)
            //    return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
    }
}
