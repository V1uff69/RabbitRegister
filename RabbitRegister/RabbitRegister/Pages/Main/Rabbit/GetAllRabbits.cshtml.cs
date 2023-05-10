using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;

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


		public void OnGet()
		{
			Rabbits = _rabbitService.GetRabbits();
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

		public IActionResult OnPostPriceFilter()
		{
			Rabbits = _rabbitService.RatingFilter(MaxRating, MinRating).ToList();
			return Page();
		}
	}
	
}
