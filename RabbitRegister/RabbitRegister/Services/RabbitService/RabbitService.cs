using Microsoft.EntityFrameworkCore;
﻿using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;

namespace RabbitRegister.Services.RabbitService
{
    public class RabbitService : IRabbitService
    {
        public static List<Rabbit> _rabbits;

        private DbGenericService<Rabbit> _dbGenericService;
        private IBreederService _breederService;

        /// <summary>
        /// Initialisere hvorfra kaninere hentes..
        /// 
        /// Kaninerne kan hentes via DB eller MOCKDATA, alt efter hvilken kodeblok,
        /// som kommenteres ud/ind.
        /// </summary>
        /// <param name="dbGenericService">Dependency injection</param>
        /// <param name="breederService">Dependency injection</param>
        public RabbitService(DbGenericService<Rabbit> dbGenericService, IBreederService breederService)
        {
            _breederService = breederService;
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

        /// <summary>
        /// Tilføjer et kanin objekt til den lokale liste: _rabbits med dens tilhørende avler.
        /// Gemmer derefter kaninen i via. en DBGenericService metode.
        /// 
        /// "Async" muliggør en mere effektiv udnyttelse af systemressourcer. Tråden som kalder AddRabbitAsync,
        /// behøver ikke at vente på operationen er fuldført, før den fortsætter med andre opgaver 
        /// </summary>
        /// <param name="rabbit">Kanin objektet som tilføjes til listen _rabbits OG tilføjes til DB via dbGenericService</param>
        /// <param name="breeder">Avler objektet, som tilhører kaninen</param>
        /// <returns>En Task, der repræsenterer asynkron udførelse af operationen</returns>
        /// 


        public async Task AddRabbitAsync(Rabbit rabbit, Breeder breeder)  //Denne add metode spørger efter Rabbit Breeder propertien -.-' 
        {
            _rabbits.Add(rabbit);
            rabbit.Breeder = breeder;

            await _dbGenericService.AddObjectAsync(rabbit);
        }



        /// <summary>
        /// Konvertere brugerens input til at passe med: class Rabbit 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="breeder"></param>
        /// <returns></returns>
        public async Task AddRabbitAsync(RabbitDTO dto, Breeder breeder)
        {
            Rabbit newRabbit = new Rabbit();

            newRabbit.RabbitRegNo = dto.RabbitRegNo;
            newRabbit.OriginRegNo = dto.OriginRegNo;
            newRabbit.Owner = dto.Owner;
            newRabbit.Name = dto.Name;
            newRabbit.Race = dto.Race;
            newRabbit.Color = dto.Color;
            newRabbit.DateOfBirth = dto.DateOfBirth;
            newRabbit.DeadOrAlive = dto.DeadOrAlive;
            newRabbit.Sex = dto.Sex;
            newRabbit.IsForSale = dto.IsForSale;
            newRabbit.ImageString = dto.ImageString;

            //newRabbit.Breeder = breeder;
            
            _rabbits.Add(newRabbit);
            await _dbGenericService.AddObjectAsync(newRabbit);

            if (breeder.Rabbits == null)
            {
                breeder.Rabbits = new List<Rabbit>();
            }

            breeder.Rabbits.Add(newRabbit);

        }


        /// <summary>
        /// Henter en kanin fra listen _rabbits via. LAMBDA og LINQ ud fra dens composite-key
        /// </summary>
        /// <param name="id">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="OriginRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Et kanin objekt</returns>
        public Rabbit GetRabbit(int id, int originRegNo)
        {
            return _rabbits.Find(r => r.RabbitRegNo == id && r.OriginRegNo == originRegNo);
        }

        /// <summary>
        /// Finder alle kaniner som er i listen _rabbits
        /// </summary>
        /// <returns></returns>
        public List<Rabbit> GetAllRabbits()
        {
            return _rabbits.ToList();
            //return _dbGenericService.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// Opdaterer alle kaninens oplysninger, med udgangspunkt i et kanin objekt og dets komposite key.
        /// </summary>
        /// <param name="rabbit">Kaninen som opdateres</param>
        /// <param name="id">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="originRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Asynkron Task, der repræsenterer opdateringsoperationen</returns>
        public async Task UpdateRabbitAsync(Rabbit rabbit, int id, int originRegNo)
        {
            if (rabbit != null)
            {
                Rabbit existingRabbit = _rabbits.FirstOrDefault(r => r.RabbitRegNo == id && r.OriginRegNo == originRegNo);
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

        public async Task ChangeOwnership(Rabbit rabbit, int oldOwner, int newOwner)
        {

        }

        /// <summary>
        /// Sletter en kanin baseret på dens angivne composite-key.
        /// 
        /// Første kodeblok finder, og definere den specifikke kanin som skal slettes via dets Key.
        /// Anden kodeblok fjerner kaninen fra listen _rabbits, og afventer asynkront på objektet
        /// kan slettes fra DB
        /// </summary>
        /// <param name="id">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="originRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Task, der repræsenterer sletningsoperationen</returns>
        public async Task<Rabbit> DeleteRabbitAsync(int? id, int? originRegNo)
        {
            Rabbit rabbitToBeDeleted = null;
            foreach (Rabbit rabbit in _rabbits)
            {
                if (rabbit.RabbitRegNo == id && rabbit.OriginRegNo == originRegNo)
                {
                    rabbitToBeDeleted = rabbit;
                    break;      //Dette optimere koden så den ikke fortsætter med at iterere igennem
                }
            }

            if (rabbitToBeDeleted != null)
            {
                _rabbits.Remove(rabbitToBeDeleted);
                await _dbGenericService.DeleteObjectAsync(rabbitToBeDeleted);
            }

            return rabbitToBeDeleted;
        }

        /// <summary>
        /// Udfører en søgning efter kaniner baseret på deres navne vi LINQ.
        /// Er stringen tom/null returneres listen _rabbits
        /// 
        /// NB: Denne kode er ikke implementeret/færdig da den ikke er testet
        /// for om den returnere andre avlereres kaniner!
        /// </summary>
        /// <param name="str">String, der skal matches med kanin navn</param>
        /// <returns>En samling af kaniner, der matcher stringen</returns>
        public IEnumerable<Rabbit> SearchByName(string str)
        {
            if (string.IsNullOrEmpty(str))
                return _rabbits;

            return from rabbit in _rabbits
                   where rabbit.Name.ToLower().Contains(str.ToLower())
                   select rabbit;
        }


        public IEnumerable<Rabbit> RatingFilter(int? maxRating, int? minRating)    //LINQ (_rabbits.where) && LAMDA (rabbit => )
        {
            return _rabbits.Where(
                rabbit => (minRating == 0 || rabbit.Rating >= minRating) &&
                (maxRating == 0 || rabbit.Rating <= maxRating));
        }

        //----: KODE SEKTION SOM IKKE BLEV FÆRDIG/RELEVANT :---- 
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
        //END: KODE SEKTION SOM IKKE BLEV FÆRDIG/RELEVANT :---- 


        //---: SEKTION FOR ONGET() METODER :---

        /// <summary>
        /// Henter alle de kaniner som Avleren har specificeret er til salg via. LAMBDA og LINQ
        /// </summary>
        /// <returns>En liste af kaniner som forskellige avlerere har sat til salg</returns>
        public List<Rabbit> GetIsForSaleRabbits()
        {
            return _rabbits.Where(rabbit => rabbit.IsForSale == IsForSale.Ja).ToList();
        }

        /// <summary>
        /// Default OnGet() metoden som kaldes når en avler tilgår GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i brugerens avler-ID og kæder det sammen med de kaniner,
        /// avleren ejer, som er i live.
        /// </summary>
        /// <param name="OriginRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, som er levende</returns>
        public List<Rabbit> GetOwnedAliveRabbits(int originRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == originRegNo && rabbit.DeadOrAlive == DeadOrAlive.Levende).ToList();
        }

        /// <summary>
        /// Funktion som tilgåes via knappen "Filter Kaniner" på GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i brugerens avler-ID og kæder det sammen med de kaniner,
        /// avleren ejer, som er døde.
        /// </summary>
        /// <param name="OriginRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, som er døde</returns>
        public List<Rabbit> GetOwnedDeadRabbits(int originRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == originRegNo && rabbit.DeadOrAlive == DeadOrAlive.Død).ToList();
        }

        /// <summary>
        /// Funktion som tilgåes via knappen "Filter Kaniner" på GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i brugerens avler-ID og kæder det sammen med de kaniner,
        /// avleren ejer ELLER har forbindelse til (kaniner med avlerens ID)
        /// </summary>
        /// <param name="originRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, og kaniner med avlerens ID</returns>
        public List<Rabbit> GetAllRabbitsWithConnectionsToMe(int originRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == originRegNo || rabbit.OriginRegNo == originRegNo).ToList();
        }

        public virtual List<Rabbit> GetAllRabbitsWithOwner(int Owner)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == Owner).ToList();
        }

        /// <summary>
        /// Funktion som tilgåes via knappen "Filter Kaniner" på GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i alle kaniner som kommer fra avlerens stald,
        /// men som ikke længere ejes af avleren.
        /// </summary>
        /// <param name="originRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af kaniner, som avleren IKKE ejer, men er rigistreret med avlerens avler-ID</returns>
        public List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int originRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner != originRegNo && rabbit.OriginRegNo == originRegNo).ToList();
        }


    }
}
