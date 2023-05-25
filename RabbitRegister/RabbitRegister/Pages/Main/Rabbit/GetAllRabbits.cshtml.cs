using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Pages.Main.LogIn;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.BreederService;
using Microsoft.AspNetCore.Authorization;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class GetAllRabbitsModel : PageModel
    {
        private IRabbitService _rabbitService;

        public GetAllRabbitsModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        public List<Model.Rabbit>? Rabbits { get; private set; }

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public int MinRating { get; set; }


        public IActionResult OnGet(int breederRegNo, string action)
        {
            if (action == "OwnedDeadRabbits")
            {
                Rabbits = _rabbitService.GetOwnedDeadRabbits(breederRegNo);
            }          
            else if (action == "AllRabbitsWithConnectionsToMe")
            {
                Rabbits = _rabbitService.GetAllRabbitsWithConnectionsToMe(breederRegNo);
            }
            else if (action == "NotOwnedRabbitsWithMyBreederRegNo")
            {
                Rabbits = _rabbitService.GetNotOwnedRabbitsWithMyBreederRegNo(breederRegNo);
            }           
            else
            {
                Rabbits = _rabbitService.GetOwnedAliveRabbits(breederRegNo);
            }

            return Page();
        }


        public IActionResult OnPostNameSearch()
        {
            Rabbits = _rabbitService.NameSearch(SearchString).ToList();
            return Page();
        }

  
    }
}
