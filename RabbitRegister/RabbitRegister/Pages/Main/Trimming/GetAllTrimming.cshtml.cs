using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    public class GetAllTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;
        public GetAllTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        public List<Model.Trimming>? trimmings { get; set; }
        public void OnGet()
        {
            trimmings = _trimmingService.GetTrimmings();
        }
    }
}
