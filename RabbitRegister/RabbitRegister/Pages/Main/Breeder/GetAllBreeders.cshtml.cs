using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace RabbitRegister.Pages.Main.Breeder
{
    [Authorize(Roles = "Admin")] // Kun brugere med rollen "Admin" til at få adgang til denne side
    public class GetAllBreedersModel : PageModel
    {
        private IBreederService _breederService; // Reference til IBreederService, der bruges til at håndtere avlerrelaterede metoder osv

        [BindProperty]
        public string SearchBreederRegNo { get; set; }

        [BindProperty]
        public string SearchName { get; set; }

        public GetAllBreedersModel(IBreederService breederService)
        {
            _breederService = breederService; // Gemmer en reference til IBreederService, der er injiceret
        }

        public List<Model.Breeder>? Breeders { get; set; } // Egenskab til at gemme en liste over avlere

        public void OnGet()
        {
            Breeders = _breederService.GetBreeders(); // Henter alle avlere fra databasen ved at kalde GetBreeders-metoden på IBreederService og gemmer dem i Breeders-egenskaben
        }

        public IActionResult OnPostSearchByBreederRegNo()
        {
            Breeders = _breederService.SearchByBreederRegNo(SearchBreederRegNo).ToList();
            return Page();
        }

        public IActionResult OnPostSearchByBreederName()
        {
            Breeders = _breederService.SearchByBreederName(SearchName).ToList();
            return Page();
        }
    }
}
