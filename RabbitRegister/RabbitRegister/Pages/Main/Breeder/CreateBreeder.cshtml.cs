using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Pages.Main.Breeder;

namespace RabbitRegister
{
    public class CreateBreederModel : PageModel
    {
		private IBreederService _breederService;
		

		public CreateBreederModel(IBreederService breederService)
		{
			_breederService = breederService;
		}
		
		[BindProperty]
		public Breeder Breeder { get; set; } = new Breeder();

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(Breeder breeder)
		{
			await _breederService.AddUserAsync(breeder);
			return RedirectToPage("GetAllBreeders");
		}
	}
}
