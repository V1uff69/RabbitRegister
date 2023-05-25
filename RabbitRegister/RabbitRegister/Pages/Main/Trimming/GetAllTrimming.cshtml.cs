using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services.TrimmingService;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RabbitRegister.Pages.Main.Trimming
{
    [Authorize(Policy = "BreederOnly")]
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
        [BindProperty]
        public int OwnerId { get; set; }

        public void OnGet(int RabbitRegNo, int BreederRegNo, int? Owner)
        {
            if (Owner != null && Owner > 0)
            {
                OwnerId = (int)Owner;
                Trimmings = _trimmingService.GetTrimmingsByOwnerId(OwnerId);
            }
            else if (RabbitRegNo > 0 && BreederRegNo > 0)
            {
                Trimmings = _trimmingService.GetTrimmingByRabbitRegNoAndBreederRegNo(RabbitRegNo, BreederRegNo);
            }
            else
            {
                Trimmings = new List<Model.Trimming>();
            }
        }
        public IActionResult OnGetSortById(int Owner)
        {
            Trimmings = _trimmingService.SortById(Owner).ToList();
            return Page();
        }

        public IActionResult OnGetSortByIdDescending(int Owner)
        {
            Trimmings = _trimmingService.SortByIdDescending(Owner).ToList();
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

        public IActionResult OnGetSortByDate(int Owner)
        {
            Trimmings = _trimmingService.SortByDate(Owner).ToList();
            return Page();
        }

        public IActionResult OnGetSortByDateDescending(int Owner)
        {
            Trimmings = _trimmingService.SortByDateDescending(Owner).ToList();
            return Page();
        }

        public IActionResult OnPostNameSearch(int Owner)
        {
            Trimmings = _trimmingService.NameSearch(SearchString, Owner).ToList();
            return Page();
        }

        public IActionResult OnPostRabbitIdSearch()
        {
            Trimmings = _trimmingService.RabbitIdSearch(SearchId).ToList();
            return Page();
        }
    }
}
