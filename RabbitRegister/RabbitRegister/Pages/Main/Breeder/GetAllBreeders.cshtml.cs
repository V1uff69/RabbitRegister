using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authorization;


namespace RabbitRegister.Pages.Main.Breeder
{
    [Authorize(Roles = "Admin")]
    public class GetAllBreedersModel : PageModel
    {
        private IBreederService _breederService;

        public GetAllBreedersModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        public List<Model.Breeder>? Breeders { get; set; }


        public void OnGet()
        {
            Breeders = _breederService.GetBreeders();
        }
    }
}
