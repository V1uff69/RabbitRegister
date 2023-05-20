using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    public class RabbitProfileModel : PageModel
    {
        private IRabbitService _rabbitService;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Race { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public Sex Sex { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        public float Weight { get; set; }

        [BindProperty]
        public float Rating { get; set; }

        [BindProperty]
        public DeadOrAlive DeadOrAlive { get; set; }

        [BindProperty]
        public IsForSale IsForSale { get; set; }

        [BindProperty]
        public string SuitableForBreeding { get; set; }

        [BindProperty]
        public string CauseOfDeath { get; set; }

        [BindProperty]
        public string Comments { get; set; }

        [BindProperty]
        public string ImageString { get; set; }

        public RabbitProfileModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet(int id)
        {
            Rabbit = _rabbitService.GetRabbit(id);
            //if (Rabbit == null)
            //    return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
    }
}
