using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    public class DeleteRabbitModel : PageModel
    {
		private IRabbitService _rabbitService;

		public DeleteRabbitModel(IRabbitService rabbitService)
		{
			_rabbitService = rabbitService;
		}

		[BindProperty]
		public Model.Rabbit Rabbit { get; set; }


		public IActionResult OnGet(int id)
		{
			Rabbit = _rabbitService.GetRabbit(id);
			//if (Rabbit == null)
			//	return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			Model.Rabbit deletedRabbit = await _rabbitService.DeleteRabbitAsync(id);
			//if (deletedRabbit == null)
			//	return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return RedirectToPage("GetAllRabbits");
		}
	}
}
