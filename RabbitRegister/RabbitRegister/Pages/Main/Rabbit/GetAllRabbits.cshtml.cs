using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Pages.Main.LogIn;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.BreederService;

namespace RabbitRegister.Pages.Main.Rabbit
{
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

        [BindProperty]
        public int MaxRating { get; set; }


        public IActionResult OnGet(int breederRegNo, string action)
        {
            if (action == "OwnedDeadRabbits")
            {
                Rabbits = _rabbitService.GetOwnedDeadRabbits(breederRegNo);
            }

            else if (action == "AllOwnedRabbits")
            {
                Rabbits = _rabbitService.GetAllOwnedRabbits(breederRegNo);
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

        public IActionResult OnGetSortById()
        {
            Rabbits = _rabbitService.SortById().ToList();
            return Page();
        }

        public IActionResult OnGetSortByIdDescending()
        {
            Rabbits = _rabbitService.SortByIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByName()
        {
            Rabbits = _rabbitService.SortByName().ToList();
            return Page();
        }

        public IActionResult OnGetSortByNameDescending()
        {
            Rabbits = _rabbitService.SortByNameDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRating()
        {
            Rabbits = _rabbitService.SortByRating().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRatingDescending()
        {
            Rabbits = _rabbitService.SortByRatingDescending().ToList();
            return Page();
        }

        public IActionResult OnPostNameSearch()
        {
            Rabbits = _rabbitService.NameSearch(SearchString).ToList();
            return Page();
        }

        public IActionResult OnPostRatingFilter()
        {
            Rabbits = _rabbitService.RatingFilter(MaxRating, MinRating).ToList();
            return Page();
        }
    }
}
