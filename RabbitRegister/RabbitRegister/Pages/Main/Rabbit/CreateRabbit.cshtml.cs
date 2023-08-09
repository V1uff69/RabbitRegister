using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Services.RabbitService;
using System.ComponentModel.DataAnnotations;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class CreateRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;
        private IBreederService _breederService;

        public CreateRabbitModel(IRabbitService rabbitService, IBreederService breederService)
        {
            _rabbitService = rabbitService;
            _breederService = breederService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; } = new Model.Rabbit();
        //public Model.Breeder Breeder = User.Identity.Name;

        public bool exceptionFound { get; set; }
        public string exceptionText { get; set; }

        /// <summary>
        /// Forsøger at oprette en kanin, hvis ikke ID allerede findes
        /// </summary>
        /// <param name="Rabbit">Kanin objekt som skal tilføjes</param>
        /// <returns>Forsøger at tilføje en kanin og returnere avleren til GetAllRabbits med sit Avler-ID</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            //Denne del kigger på brugerens INPUT er korrekt udført.NB: IKKE om kanin med samme ID eksistere
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Nedenstående sikrer der ikke oprettes en fantom-kanin på "GetAllRabbits"
            var existingRabbit = _rabbitService.GetRabbit(Rabbit.RabbitRegNo, Rabbit.BreederRegNo);
            if (existingRabbit != null)
            {
                this.exceptionFound = true;
                this.exceptionText = "En Kanin med samme ID findes allerede";
                return Page();
            }

            var breeder = await _breederService.GetBreederByNameAsync(User.Identity.Name);
            await _rabbitService.AddRabbitAsync(Rabbit, breeder);
            return RedirectToPage("GetAllRabbits", new { breederRegNo = User.Identity.Name });

        }

    }
}
