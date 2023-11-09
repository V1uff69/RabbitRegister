using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Migrations;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using System.Diagnostics;
using System.Drawing;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class EditRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;

        public EditRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public RabbitDTO RabbitDTO { get; set; }

        public IActionResult OnGet(int rabbitRegNo, int originRegNo)
        {
            var existingRabbit = _rabbitService.GetRabbit(rabbitRegNo, originRegNo);

            if (existingRabbit == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != existingRabbit.Owner.ToString())
            {
                return Forbid();
            }

            // Kopier data fra eksisterende Rabbit til RabbitDTO
            RabbitDTO = new RabbitDTO
            {
                RabbitRegNo = existingRabbit.RabbitRegNo,
                OriginRegNo = existingRabbit.OriginRegNo,
                // Kopier de øvrige egenskaber her
                Name = existingRabbit.Name,
                Race = existingRabbit.Race,
                Color = existingRabbit.Color,
                // Kopier resten af egenskaberne
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int rabbitRegNo, int originRegNo)
        {
            var existingRabbit = _rabbitService.GetRabbit(rabbitRegNo, originRegNo);

            if (existingRabbit == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != existingRabbit.Owner.ToString())
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            existingRabbit.Name = RabbitDTO.Name;
            existingRabbit.Race = RabbitDTO.Race;
            existingRabbit.Color = RabbitDTO.Color;
            existingRabbit.Sex = RabbitDTO.Sex;
            existingRabbit.DateOfBirth = RabbitDTO.DateOfBirth;
            existingRabbit.Weight = RabbitDTO.Weight;
            existingRabbit.Rating = RabbitDTO.Rating;
            existingRabbit.DeadOrAlive = RabbitDTO.DeadOrAlive;
            existingRabbit.IsForSale = RabbitDTO.IsForSale;
            existingRabbit.SuitableForBreeding = RabbitDTO.SuitableForBreeding;
            existingRabbit.CauseOfDeath = RabbitDTO.CauseOfDeath;
            existingRabbit.ImageString = RabbitDTO.ImageString;           

            await _rabbitService.UpdateRabbitAsync(RabbitDTO, rabbitRegNo, originRegNo);
            return RedirectToPage("GetAllRabbits");
        }
    }
}
