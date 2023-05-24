using Microsoft.EntityFrameworkCore;
﻿using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.RabbitService
{
    public class RabbitService : IRabbitService
    {
        private List<Rabbit> _rabbits;

        private DbGenericService<Rabbit> _dbGenericService;

        public RabbitService(DbGenericService<Rabbit> dbGenericService)
        {
            _dbGenericService = dbGenericService;
            _rabbits = _dbGenericService.GetObjectsAsync().Result.ToList();

            //_rabbits = MockRabbit.GetMockRabbits(); //DB tom? Ved første Debug kør denne kode, og udkommenter igen derefter
            //foreach (var rabbit in _rabbits)
            //{
            //    _dbGenericService.AddObjectAsync(rabbit).Wait();
            //}
        }

        public RabbitService()
        {

        }

        public async Task AddRabbitAsync(Rabbit rabbit)
        {
            _rabbits.Add(rabbit);
            await _dbGenericService.AddObjectAsync(rabbit);
        }

        public Rabbit GetRabbit(int id, int breederRegNo)
        {
            return _rabbits.FirstOrDefault(r => r.RabbitRegNo == id && r.BreederRegNo == breederRegNo);
        }

        public List<Rabbit> GetAllRabbits(int id, int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.RabbitRegNo == id && rabbit.BreederRegNo == breederRegNo).ToList();
        }

        public async Task UpdateRabbitAsync(Rabbit rabbit, int id, int breederRegNo)
        {
            if (rabbit != null)
            {
                Rabbit existingRabbit = _rabbits.FirstOrDefault(r => r.RabbitRegNo == id && r.BreederRegNo == breederRegNo);
                if (existingRabbit != null)
                {
                    existingRabbit.Name = rabbit.Name;
                    existingRabbit.Race = rabbit.Race;
                    existingRabbit.Color = rabbit.Color;
                    existingRabbit.Sex = rabbit.Sex;
                    existingRabbit.DateOfBirth = rabbit.DateOfBirth;
                    existingRabbit.Weight = rabbit.Weight;
                    existingRabbit.Rating = rabbit.Rating;
                    existingRabbit.DeadOrAlive = rabbit.DeadOrAlive;
                    existingRabbit.IsForSale = rabbit.IsForSale;
                    existingRabbit.SuitableForBreeding = rabbit.SuitableForBreeding;
                    existingRabbit.CauseOfDeath = rabbit.CauseOfDeath;
                    existingRabbit.Comments = rabbit.Comments;
                    existingRabbit.ImageString = rabbit.ImageString;

                    await _dbGenericService.UpdateObjectAsync(existingRabbit);
                }
            }
        }

        public async Task<Rabbit> DeleteRabbitAsync(int? id, int? breederRegNo)
        {
            Rabbit rabbitToBeDeleted = null;
            foreach (Rabbit rabbit in _rabbits)
            {
                if (rabbit.RabbitRegNo == id && rabbit.BreederRegNo == breederRegNo)
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


        public IEnumerable<Rabbit> NameSearch(string str)
        {
            return from rabbit in _rabbits
                   orderby rabbit.Name
                   select rabbit;
        }


        public IEnumerable<Rabbit> RatingFilter(int maxRating, int minRating = 0)    //LINQ (_rabbits.where) && LAMDA (rabbit => )
        {
            return _rabbits.Where(
                rabbit => (minRating == 0 || rabbit.Rating >= minRating) &&
                (maxRating == 0 || rabbit.Rating <= maxRating));
        }


        public IEnumerable<Rabbit> SortById()     //LINQ & LAMBDA
        {
            return _rabbits.OrderBy(r => r.RabbitRegNo);   // bemærk: default er ascending(stigende.. fra 0 til 10)
        }

        public IEnumerable<Rabbit> SortByIdDescending()   //LINQ & LAMBDA 
        {
            return _rabbits.OrderByDescending(r => r.RabbitRegNo);
        }

        public IEnumerable<Rabbit> SortByName() //LINQ (for SQL og C# folket)
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

        //---: ONGET() METODER :---

        public List<Rabbit> GetIsForSaleRabbits()
        {
            return _rabbits.Where(rabbit => rabbit.IsForSale == IsForSale.Ja).ToList();
        }

        public List<Rabbit> GetOwnedAliveRabbits(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo && rabbit.DeadOrAlive == DeadOrAlive.Levende).ToList();
        }

        public List<Rabbit> GetOwnedDeadRabbits(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo && rabbit.DeadOrAlive == DeadOrAlive.Død).ToList();
        }

        public List<Rabbit> GetAllRabbitsWithConnectionsToMe(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo || rabbit.BreederRegNo == breederRegNo).ToList();
        }

        public List<Rabbit> GetAllRabbitsWithOwner(int Owner)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == Owner).ToList();
        }

        public List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner != breederRegNo && rabbit.BreederRegNo == breederRegNo).ToList();
        }


    }
}
