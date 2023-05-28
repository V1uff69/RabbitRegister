using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    public class RabbitStoreModel : PageModel
    {
        private IRabbitService _rabbitService;

        public RabbitStoreModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        public List<Model.Rabbit>? Rabbits { get; private set; }
   
        /// <summary>
        /// Henter kaniner som er sat til salg
        /// </summary>
        /// <returns>Returner brugeren til siden RabbitStore</returns>
        public IActionResult OnGet()
        {
            Rabbits = _rabbitService.GetIsForSaleRabbits();   

            return Page();
        }
    }
}

