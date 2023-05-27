using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using RabbitRegister.EFDbContext;
using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using System.Data.SqlTypes;

namespace RabbitRegister.Services.TrimmingService
{
    public class TrimmingService : ITrimmingService
    {
		/// <summary>
		/// Lists of all trimmings in DB
		/// </summary>
        private List<Trimming> _trimmings;
		/// <summary>
		/// DbGeneric Service for accessing Trimming database
		/// </summary>
		private DbGenericService<Trimming> DbGenericService;
		/// <summary>
		/// RabbitService instance for businesslogic
		/// </summary>
		private IRabbitService RabbitService; 
		public TrimmingService() { }

		/// <summary>
		/// Constructor for TrimmingService
		/// </summary>
		/// <param name="dbGenericService"></param>
		/// <param name="rabbitService"></param>
		public TrimmingService(DbGenericService<Trimming> dbGenericService, IRabbitService rabbitService)
		{
			DbGenericService = dbGenericService;
			RabbitService = rabbitService;
			_trimmings = DbGenericService.GetObjectsAsync().Result.ToList();

			//_trimmings = MockTrimming.GetMockTrimming(); //DB tom? Ved første Debug kør denne kode, og udkommenter igen derefter
			//foreach (var trimming in _trimmings)
			//{
			//	DbGenericService.AddObjectAsync(trimming).Wait();
			//}
		}
		/// <summary>
		/// Get a specific trimming by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Trimming</returns>
        public Trimming GetTrimming(int id)
		{
			foreach (Trimming trimming in _trimmings)
			{
				if (trimming.TrimmingId == id)
					return trimming;
			}
			return null;
		}
		/// <summary>
		/// Adds trimming to DB
		/// </summary>
		/// <param name="trimming"></param>
		/// <returns></returns>
		public async Task AddTrimmingAsync (Trimming trimming)
		{
            await DbGenericService.AddObjectAsync(trimming);
            _trimmings.Add(trimming);
		}
		/// <summary>
		/// Updates specific trimming based on ID
		/// </summary>
		/// <param name="trimming"></param>
		/// <param name="id"></param>
		public void UpdateTrimming(Trimming trimming, int id)
		{
			if (trimming != null)
			{
				foreach (Trimming trim in _trimmings)
				{
					if (trim.TrimmingId == id)
					{
						trim.RabbitRegNo = trimming.RabbitRegNo;
						trim.BreederRegNo = trimming.BreederRegNo;
						trim.Name = trimming.Name;
						trim.Date = trimming.Date;
						trim.TimeUsed = trimming.TimeUsed;
						trim.HairLengthByDayNinety = trimming.HairLengthByDayNinety;
						trim.WoolDensity = trimming.WoolDensity;
						trim.FirstSortmentWeight = trimming.FirstSortmentWeight;
						trim.SecondSortmentWeight = trimming.SecondSortmentWeight;
						trim.DisposableWoolWeight = trimming.DisposableWoolWeight;
						break;
					}
				}
				DbGenericService.UpdateObjectAsync(trimming);
			}
		}
		/// <summary>
		/// Deletes a specific trimming based on ID
		/// </summary>
		/// <param name="trimmingId"></param>
		/// <returns>Trimming</returns>
		public Trimming DeleteTrimming(int? trimmingId)
		{
			Trimming trimmingToBeDeleted = null;
			foreach (Trimming trimming in _trimmings)
			{
				if (trimming.TrimmingId == trimmingId)
				{
					trimmingToBeDeleted = trimming;
					break;
				}
			}

			if (trimmingToBeDeleted != null)
			{
				_trimmings.Remove(trimmingToBeDeleted);
				DbGenericService.DeleteObjectAsync(trimmingToBeDeleted);
			}
			return trimmingToBeDeleted;
		}
		/// <summary>
		/// Returns a list by a specific owner, sorted by TrimmingId ascending
		/// </summary>
		/// <param name="Owner"></param>
		/// <returns></returns>
        public IEnumerable<Trimming> SortById(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderBy(r => r.TrimmingId);
        }
		/// <summary>
		/// Returns a list by a specific owner, sorted by TrimmingId descending
		/// </summary>
		/// <param name="Owner"></param>
		/// <returns></returns>
        public IEnumerable<Trimming> SortByIdDescending(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderByDescending(r => r.TrimmingId);
        }
		/// <summary>
		/// Returns a list of all trimmings sorted by RabbitRegNo ascending
		/// </summary>
		/// <returns></returns>
        public IEnumerable<Trimming> SortByRabbitId()
        {
            return _trimmings.OrderBy(r => r.RabbitRegNo);
        }
        /// <summary>
        /// Returns a list of all trimmings sorted by RabbitRegNo decending
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Trimming> SortByRabbitIdDescending()
        {
            return _trimmings.OrderByDescending(r => r.RabbitRegNo);
        }
		/// <summary>
		/// Returns a list by a specific owner, sorted by date ascending
		/// </summary>
		/// <param name="Owner"></param>
		/// <returns></returns>
        public IEnumerable<Trimming> SortByDate(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderBy(r => r.Date);
        }
        /// <summary>
        /// Returns a list by a specific owner, sorted by date decending
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public IEnumerable<Trimming> SortByDateDescending(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderByDescending(r => r.Date);
        }
		/// <summary>
		/// Returns a list filtered by name of rabbit and owner
		/// </summary>
		/// <param name="str"></param>
		/// <param name="Owner"></param>
		/// <returns></returns>
        public IEnumerable<Trimming> NameSearch(string str, int Owner)
        {
            if (string.IsNullOrEmpty(str))
            {
                return GetTrimmingsByOwnerId(Owner);
            }
            else
            {
                return GetTrimmingsByOwnerId(Owner).Where(Trimming => Trimming.Name.ToLower().Contains(str.ToLower()));
            }
        }
		/// <summary>
		/// Returns a list filtered by RabbitId
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public IEnumerable<Trimming> RabbitIdSearch(int id)
        {
            if (id == 0)
            {
                return _trimmings;
            }
            else
            {
                return _trimmings.Where(Trimming => Trimming.RabbitRegNo == id);
            }
        }
		/// <summary>
		/// Returns a list of trimmings matching RabbitRegNo and BreederRegNo
		/// </summary>
		/// <param name="RabbitRegNo"></param>
		/// <param name="BreederRegNo"></param>
		/// <returns></returns>
		public List<Trimming> GetTrimmingByRabbitRegNoAndBreederRegNo (int RabbitRegNo, int BreederRegNo)
		{
			var Trimmings = GetTrimmings();
			List<Trimming> TrimmingByRabbitRegNo = new List<Trimming>();
			foreach (var Trimming in Trimmings)
			{
				if (Trimming.RabbitRegNo == RabbitRegNo && Trimming.BreederRegNo == BreederRegNo)
					TrimmingByRabbitRegNo.Add(Trimming);
			}
			return TrimmingByRabbitRegNo;
		}
		/// <summary>
		/// Returns a list of trimmings of each rabbit matching the OwnerId
		/// </summary>
		/// <param name="Owner"></param>
		/// <returns></returns>
		public List<Trimming> GetTrimmingsByOwnerId(int Owner)
		{
			List<Rabbit> rabbitsByOwner = RabbitService.GetAllRabbitsWithOwner(Owner);

			List<Trimming> trimmingsByOwner = new List<Trimming>();
			foreach (var Rabbit in rabbitsByOwner)
			{
				var trimmingsForRabbit = _trimmings.Where(trimming => trimming.BreederRegNo == Rabbit.BreederRegNo && trimming.RabbitRegNo == Rabbit.RabbitRegNo).ToList();

				trimmingsByOwner.AddRange(trimmingsForRabbit);
			}
			return trimmingsByOwner;
		}
		/// <summary>
		/// Returns a list of all trimmings
		/// </summary>
		/// <returns></returns>
        public List<Trimming> GetTrimmings() { return _trimmings; }
    }
}
