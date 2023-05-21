using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services.TrimmingService;
using System.Linq;
using System.Collections.Generic;

namespace RabbitRegister.Pages.Main.Trimming
{
    public class GetAllTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;
        public GetAllTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        public List<Model.Trimming>? Trimmings { get; set; }

        [BindProperty]
        public string SearchString { get; set; }
        [BindProperty]
        public int SearchId { get; set; }

        public void OnGet(int RabbitRegNo)
        {
            if (RabbitRegNo > 0)
            {
                Trimmings = _trimmingService.GetTrimmingByRabbitRegNo(RabbitRegNo);
            }
            else
            {
                Trimmings = _trimmingService.GetTrimmings();
            }
        }
        public IActionResult OnGetSortById()
        {
            Trimmings = _trimmingService.SortById().ToList();
            return Page();
        }

        public IActionResult OnGetSortByIdDescending()
        {
            Trimmings = _trimmingService.SortByIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRabbitId()
        {
            Trimmings = _trimmingService.SortByRabbitId().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRabbitIdDescending()
        {
            Trimmings = _trimmingService.SortByRabbitIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByDate()
        {
            Trimmings = _trimmingService.SortByDate().ToList();
            return Page();
        }

        public IActionResult OnGetSortByDateDescending()
        {
            Trimmings = _trimmingService.SortByDateDescending().ToList();
            return Page();
        }

        public IActionResult OnPostNameSearch()
        {
            Trimmings = _trimmingService.NameSearch(SearchString).ToList();
            return Page();
        }

        public IActionResult OnPostRabbitIdSearch()
        {
            Trimmings = _trimmingService.RabbitIdSearch(SearchId).ToList();
            return Page();
        }
    }
}
