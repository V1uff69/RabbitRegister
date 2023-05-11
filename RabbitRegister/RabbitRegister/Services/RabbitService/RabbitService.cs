﻿using RabbitRegister.Model;

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
        }

        public RabbitService()
        {

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
        public async Task UpdateRabbitAsync(Rabbit rabbit, int id) // int id tilføjet
        {
            if (rabbit != null)
            {
                foreach (Rabbit r in _rabbits)
                {
                    if (r.RabbitRegNo == id)
                    {
                        r.Name = rabbit.Name;
                        r.Race = rabbit.Race;
                        r.Color = rabbit.Color;
                        r.Sex = rabbit.Sex;
                        r.DateOfBirth = rabbit.DateOfBirth;
                        r.Weight = rabbit.Weight;
                        r.Rating = rabbit.Rating;
                        r.DeadOrAlive = rabbit.DeadOrAlive;
                        r.IsForSale = rabbit.IsForSale;
                        r.SuitableForBreeding = rabbit.SuitableForBreeding;
                        r.CauseOfDeath = rabbit.CauseOfDeath;
                        break;                                    // break tilføjet
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
        public IEnumerable<Rabbit> NameSearch(string str)
        {
            return from rabbit in _rabbits
                   orderby rabbit.Name
                   select rabbit;
        }


        public IEnumerable<Rabbit> RatingFilter(int maxRating, int minRating = 0)    //LINQ (_rabbits.where) && LAMDA (rabbit => )
        {
            return _rabbits.Where(
                item => (minRating == 0 || item.Rating >= minRating) &&
                (maxRating == 0 || item.Rating <= maxRating));
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

        public List<Rabbit> GetRabbits() { return _rabbits; }
    }
}