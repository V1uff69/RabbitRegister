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
    /// <summary>
    /// claim role , so only breeders can see page.
    /// </summary>
    [Authorize(Policy = "BreederOnly")]
    public class GetAllTrimmingModel : PageModel
    {
        /// <summary>
        /// TrimmingService instance
        /// </summary>
        private ITrimmingService _trimmingService;
        /// <summary>
        /// default constructor for GetAllTrimmingModel
        /// </summary>
        /// <param name="trimmingService"></param>
        public GetAllTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        /// <summary>
        /// List of visible Trimmings
        /// </summary>
        public List<Model.Trimming>? Trimmings { get; set; }
        /// <summary>
        /// SearchString bound to search text field
        /// </summary>
        [BindProperty]
        public string SearchString { get; set; }
        /// <summary>
        /// Search int bound to search int field
        /// </summary>
        [BindProperty]
        public int SearchId { get; set; }
        /// <summary>
        /// Reference to the logged in user/owner
        /// </summary>
        [BindProperty]
        public int OwnerId { get; set; }
        /// <summary>
        /// When page loads, we initialize Trimmings, either by:
        /// - Owner
        /// - RabbitRegNo and BreederRegNo
        /// - Or default/empty
        /// </summary>
        /// <param name="RabbitRegNo"></param>
        /// <param name="BreederRegNo"></param>
        /// <param name="Owner"></param>
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
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortById(int Owner)
        {
            Trimmings = _trimmingService.SortById(Owner).ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortByIdDescending(int Owner)
        {
            Trimmings = _trimmingService.SortByIdDescending(Owner).ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortByRabbitId()
        {
            Trimmings = _trimmingService.SortByRabbitId().ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortByRabbitIdDescending()
        {
            Trimmings = _trimmingService.SortByRabbitIdDescending().ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortByDate(int Owner)
        {
            Trimmings = _trimmingService.SortByDate(Owner).ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML sort button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnGetSortByDateDescending(int Owner)
        {
            Trimmings = _trimmingService.SortByDateDescending(Owner).ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML Search button
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IActionResult OnPostNameSearch(int Owner)
        {
            Trimmings = _trimmingService.NameSearch(SearchString, Owner).ToList();
            return Page();
        }
        /// <summary>
        /// ActionResult helping function called by HTML Search button
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostRabbitIdSearch()
        {
            Trimmings = _trimmingService.RabbitIdSearch(SearchId).ToList();
            return Page();
        }
    }
}
