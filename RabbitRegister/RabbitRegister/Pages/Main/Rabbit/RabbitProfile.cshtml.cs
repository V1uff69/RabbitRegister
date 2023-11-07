using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace RabbitRegister.Pages.Main.Rabbit
{
    public class RabbitProfileModel : PageModel
    {
        private IRabbitService _rabbitService;

        public RabbitProfileModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet(int rabbitRegNo, int originRegNo)
        {
            Rabbit = _rabbitService.GetRabbit(rabbitRegNo, originRegNo);

            if (Rabbit == null)
            {
                return RedirectToPage("/NotFound");
            }

            if (User.Identity.Name == Rabbit.Owner.ToString() || User.Identity.Name == Rabbit.OriginRegNo.ToString() || Rabbit.IsForSale == IsForSale.Ja)
            {
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

    }
}
