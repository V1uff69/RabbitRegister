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

        public IActionResult OnGet(int id, int originRegNo)
        {
            // Først, fjern dette. Det er ikke nødvendigt og korrekt.
            // int Rabbit.Owner = int.Parse(User.Identity.Name);

            // Hent kaninen fra din service.
            Rabbit = _rabbitService.GetRabbit(id, originRegNo);

            // Tjek om brugeren er ejeren af kaninen (hvis Owner er det samme som brugerens BreederRegNo)
            if (Rabbit != null && User.Identity.Name == Rabbit.Owner.ToString())
            {
                // Brugeren er ejer af kaninen, så de har adgang.
                return Page();
            }
            // Tjek om brugerens BreederRegNo matcher kaninens OriginRegNo
            else if (Rabbit != null && User.Identity.Name == Rabbit.OriginRegNo.ToString())
            {
                // Brugeren har adgang, fordi deres BreederRegNo matcher kaninens OriginRegNo.
                return Page();
            }
            else
            {
                //return Unauthorized();
                return RedirectToPage("/Account/AccessDenied");

                // Du kan også omdirigere dem til en anden side ved at bruge: return RedirectToPage("/AccessDenied");
            }
        }


        //public IActionResult OnGet(int id, int originRegNo)
        //{
        //    Rabbit = _rabbitService.GetRabbit(id, originRegNo);

        //    return Page();
        //}
    }
}
