using Microsoft.AspNetCore.Mvc.Rendering;
using RabbitRegister.Model;
using System.Linq;

namespace RabbitRegister.Services.RabbitService
{
	public class RabbitService : IRabbitService
	{

		private List<Rabbit> _rabbits;
		//private DbService DbService { get; set; }
		private DbGenericService<Rabbit> _dbGenericService;

		public RabbitService(DbGenericService<Rabbit> dbGenericService)
		{
			_dbGenericService = dbGenericService;
			//DbService = dbService;
			// _rabbits = MockRabbits.GetMockRabbits();
			_rabbits = _dbGenericService.GetObjectsAsync().Result.ToList();
			//_rabbits = DbService.GetRabbits().Result;
			//_dbGenericService.SaveObjects(_items);  // Bruges KUN første gang ved SAVE fra JSON til DB
		}

		public RabbitService()
		{
			//_rabbits = MockData.MockRabbits.GetMockRabbits();
		}

		public async Task AddRabbitAsync(Rabbit rabbit)
		{
			_rabbits.Add(rabbit);
			//DbService.AddRabbit(rabbit);
			await _dbGenericService.AddObjectAsync(rabbit);
		}

		public Rabbit GetRabbit(int id)
		{
			foreach (Rabbit rabbit in _rabbits)
			{
				if (rabbit.RabbitRegNo == id)
					return rabbit;
			}

			return null;
		}

		public async Task UpdateRabbitAsync(Rabbit rabbit)
		{
			if (rabbit != null)
			{
				foreach (Rabbit r in _rabbits)
				{
					if (r.RabbitRegNo == rabbit.RabbitRegNo)
					{
						r.Name = rabbit.Name;
						r.Rating = rabbit.Rating;
					}
				}
				await _dbGenericService.UpdateObjectAsync(rabbit);
			}
		}

		public async Task<Rabbit> DeleteRabbitAsync(int? rabbitId)
		{
			Rabbit rabbitToBeDeleted = null;
			foreach (Rabbit rabbit in _rabbits)
			{
				if (rabbit.RabbitRegNo == rabbitId)
				{
					rabbitToBeDeleted = rabbit;
					break;
				}
			}

			if (rabbitToBeDeleted != null)
			{
				_rabbits.Remove(rabbitToBeDeleted);
				await _dbGenericService.DeleteObjectAsync(rabbitToBeDeleted);
			}

			return rabbitToBeDeleted;
		}

		public List<Rabbit> GetRabbits() { return _rabbits; }


		public IEnumerable<Rabbit> NameSearchLinq(string str)         //LINQ SEARCH
		{
			return from rabbit in _rabbits
				   orderby rabbit.Name
				   select rabbit;
		}

		public IEnumerable<Rabbit> NameSearch(string str)         //LINQ(.Where) & LAMBDA     <--
		{
			return _rabbits.Where(obj => string.IsNullOrEmpty(str) || obj.Name.ToLower().Contains(str.ToLower()));
		}

		public IEnumerable<Rabbit> NameSearchLamda(string str)     //LAMBDA SEARCH
		{
			return _rabbits.FindAll(obj => string.IsNullOrEmpty(str) || obj.Name.ToLower().Contains(str.ToLower()));
		}


		//------------------------------------------------------------------------------


		/// Step 11 (PriceFilter)
		/// Refaktorer PriceFilter så den benytter LINQ i stedet for FindAll() med et Lambda-expressions som prædicat - function.

		public IEnumerable<Rabbit> RatingFilter(int maxRating, int minRating = 0)    //LINQ (_items.where) && LAMDA (item => )
		{
			return _rabbits.Where(
				item => (minRating == 0 || item.Rating >= minRating) &&
				(maxRating == 0 || item.Rating <= maxRating));
		}

		public IEnumerable<Rabbit> RatingFilterLAMBDA(int maxRating, int minRating = 0)
		{
			return _rabbits.FindAll(
				obj => (minRating == 0 && obj.Rating <= maxRating) ||
					   (maxRating == 0 && obj.Rating >= minRating) ||
					   (obj.Rating >= minRating && obj.Rating <= maxRating));
		}


		///https://linqsamples.com/linq-to-objects/ordering/OrderByDescending-linq 

		public IEnumerable<Rabbit> SortById()     //LINQ & LAMBDA
		{
			return _rabbits.OrderBy(r => r.RabbitRegNo);   // bemærk: default er ascending(stigende.. fra 0 til 10)
		}



		public IEnumerable<Rabbit> SortByIdDescending()   //LINQ & LAMBDA 
		{
			return _rabbits.OrderByDescending(r => r.RabbitRegNo);
		}

		public IEnumerable<Rabbit> SortByName()   //LINQ (.OrderBy) & LAMBDA
		{
			return _rabbits.OrderBy(obj => obj.Name);
		}

		public IEnumerable<Rabbit> SortByNameLINQ() //LINQ (for SQL og C# folket)
		{
			return from rabbit in _rabbits
				   orderby rabbit.Name
				   select rabbit;
		}

		public IEnumerable<Rabbit> SortByNameDescending() //LAMBDA (for C# og Java folket)
		{
			return _rabbits.OrderByDescending(obj => obj.Name);
		}

		public IEnumerable<Rabbit> SortByNameDescending2() //LINQ (for SQL og C# folket)
		{
			return from rabbit in _rabbits
				   orderby rabbit.Name descending
				   select rabbit;
		}


		public IEnumerable<Rabbit> SortByRating()  //LINQ (for SQL og C# folket)
		{
			return from rabbit in _rabbits
				   orderby rabbit.Rating
				   select rabbit;
		}


		public IEnumerable<Rabbit> SortByRatingDescending() //LAMBDA (for C# og Java folket)
		{
			return _rabbits.OrderByDescending(obj => obj.Rating);
		}


		public async Task<List<string>> GetValidColorsForAngoraAsync()
		{
			return await Task.FromResult(new List<string> { "Agouti", "Chinchilla", "JapanskBlå", "Sort", "Blå", "Brun", "Lilac", "Rød", "Californisk Hvid", "Havana", "Isabella", "Madagaskar" });
		}

		public async Task<List<string>> GetValidColorsForSatinAngoraAsync()
		{
			return await Task.FromResult(new List<string> { "Agouti", "Chinchilla", "JapanskBlå", "Sort", "Blå", "Brun", "Lilac", "Rød", "Californisk Hvid" });
		}

		public async Task<List<string>> GetValidColorsForRaceAsync(RabbitRace race)
		{
			switch (race)
			{
				case RabbitRace.Angora:
					return await GetValidColorsForAngoraAsync();
				case RabbitRace.SatinAngora:
					return await GetValidColorsForSatinAngoraAsync();
				default:
					return new List<string>();
			}
		}	

	}

}
