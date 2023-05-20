using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class EditRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Race { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public Sex Sex { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        public float Weight { get; set; }

        [BindProperty]
        public float Rating { get; set; }

        [BindProperty]
        public DeadOrAlive DeadOrAlive { get; set; }

        [BindProperty]
        public IsForSale IsForSale { get; set; }

        [BindProperty]
        public string SuitableForBreeding { get; set; }

        [BindProperty]
        public string CauseOfDeath { get; set; }

        [BindProperty]
        public string Comments { get; set; }

        [BindProperty]
        public string ImageString { get; set; }

        public EditRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet(int id)
        {
            Rabbit = _rabbitService.GetRabbit(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var rabbit = _rabbitService.GetRabbit(id);
            rabbit.Name = Name;
            rabbit.Race = Race;
            rabbit.Color = Color;
            rabbit.Sex = Sex;
            rabbit.DateOfBirth = DateOfBirth;
            rabbit.Weight = Weight;
            rabbit.Rating = Rating;
            rabbit.DeadOrAlive = DeadOrAlive;
            rabbit.IsForSale = IsForSale;
            rabbit.SuitableForBreeding = SuitableForBreeding;
            rabbit.CauseOfDeath = CauseOfDeath;
            rabbit.Comments = Comments;
            rabbit.ImageString = ImageString;

            await _rabbitService.UpdateRabbitAsync(rabbit, id);
            return RedirectToPage("/Main/GetAllRabbits");
        }
    }
}
