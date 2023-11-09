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
        public Rabbit GetRabbit(int rabbitRegNo, int originRegNo)
        {
            return _rabbits.Find(r => r.RabbitRegNo == rabbitRegNo && r.OriginRegNo == originRegNo);
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
        /// <param name="rabbitRegNo">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="originRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Asynkron Task, der repræsenterer opdateringsoperationen</returns>
        //public async Task UpdateRabbitAsync(Rabbit rabbit, int rabbitRegNo, int originRegNo)
        //{
        //    if (rabbit != null)
        //    {
        //        Rabbit existingRabbit = _rabbits.FirstOrDefault(r => r.RabbitRegNo == rabbitRegNo && r.OriginRegNo == originRegNo);
        //        if (existingRabbit != null)
        //        {
        //            existingRabbit.Name = rabbit.Name;
        //            existingRabbit.Race = rabbit.Race;
        //            existingRabbit.Color = rabbit.Color;
        //            existingRabbit.Sex = rabbit.Sex;
        //            existingRabbit.DateOfBirth = rabbit.DateOfBirth;
        //            existingRabbit.Weight = rabbit.Weight;
        //            existingRabbit.Rating = rabbit.Rating;
        //            existingRabbit.DeadOrAlive = rabbit.DeadOrAlive;
        //            existingRabbit.IsForSale = rabbit.IsForSale;
        //            existingRabbit.SuitableForBreeding = rabbit.SuitableForBreeding;
        //            existingRabbit.CauseOfDeath = rabbit.CauseOfDeath;
        //            existingRabbit.Comments = rabbit.Comments;
        //            existingRabbit.ImageString = rabbit.ImageString;

        //            await _dbGenericService.UpdateObjectAsync(existingRabbit);
        //        }
        //    }
        //}

        public async Task UpdateRabbitAsync(RabbitDTO rabbitDTO, int rabbitRegNo, int originRegNo)
        {
            if (rabbitDTO != null)
            {
                Rabbit existingRabbit = _rabbits.FirstOrDefault(r => r.RabbitRegNo == rabbitRegNo && r.OriginRegNo == originRegNo);
                if (existingRabbit != null)
                {
                    // Kopier data fra RabbitDTO til det eksisterende Rabbit-objekt
                    existingRabbit.Name = rabbitDTO.Name;
                    existingRabbit.Race = rabbitDTO.Race;
                    existingRabbit.Color = rabbitDTO.Color;
                    existingRabbit.Sex = rabbitDTO.Sex;
                    existingRabbit.DateOfBirth = rabbitDTO.DateOfBirth;
                    existingRabbit.Weight = rabbitDTO.Weight;
                    existingRabbit.Rating = rabbitDTO.Rating;
                    existingRabbit.DeadOrAlive = rabbitDTO.DeadOrAlive;
                    existingRabbit.IsForSale = rabbitDTO.IsForSale;
                    existingRabbit.SuitableForBreeding = rabbitDTO.SuitableForBreeding;
                    existingRabbit.CauseOfDeath = rabbitDTO.CauseOfDeath;
                    existingRabbit.Comments = rabbitDTO.Comments;
                    existingRabbit.ImageString = rabbitDTO.ImageString;

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
        /// <param name="rabbitRegNo">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="originRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Task, der repræsenterer sletningsoperationen</returns>
        public async Task<Rabbit> DeleteRabbitAsync(int? rabbitRegNo, int? originRegNo)
        {
            Rabbit rabbitToBeDeleted = null;
            foreach (Rabbit rabbit in _rabbits)
            {
                if (rabbit.RabbitRegNo == rabbitRegNo && rabbit.OriginRegNo == originRegNo)
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
        /// Intern filter søgefunktion for avlerens egne kaniner.
        /// 
        /// Denne søgefunktion finder kaniner hvis navn passer med søgestrengen.
        /// Der søges kun blandt de kaniner som avleren ejer(+ døde) eller kaniner som stammer fra avleren.
        /// </summary>
        /// <param name="str">Søge kriterie for navn</param>
        /// <param name="breederRegNo">Avler-ID reference</param>
        /// <returns></returns>
        public IEnumerable<Rabbit> SearchByName(string str, int breederRegNo)
        {
            var rabbitsWithConnections = GetAllRabbitsWithConnectionsToMe(breederRegNo);

            if (string.IsNullOrEmpty(str))
            {
                return rabbitsWithConnections;
            }

            return rabbitsWithConnections
                .Where(rabbit => rabbit.Name.ToLower().Contains(str.ToLower()));
        }


        public IEnumerable<Rabbit> RatingFilter(int breederRegNo, int? maxRating = null, int? minRating = null)
        {
            var rabbitsWithConnections = GetAllRabbitsWithConnectionsToMe(breederRegNo);

            return rabbitsWithConnections
                .Where(rabbit =>
                    (!minRating.HasValue || rabbit.Rating >= minRating) &&
                    (!maxRating.HasValue || rabbit.Rating <= maxRating)
                );
        }

        public IEnumerable<Rabbit> SearchByRegNo(int? rabbitRegNo, int? originRegNo, int breederRegNo)
        {
            var rabbitsWithConnections = GetAllRabbitsWithConnectionsToMe(breederRegNo);

            return rabbitsWithConnections
                .Where(rabbit =>
                    (!rabbitRegNo.HasValue || rabbit.RabbitRegNo == rabbitRegNo) &&
                    (!originRegNo.HasValue || rabbit.OriginRegNo == originRegNo)
                );
        }

        //----: ENDNU IKKE IMPLEMENTERET KODE. FORTSAT RELEVANT? :---- 
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

        public IEnumerable<Rabbit> SortByNameDescending() //LAMBDA
        {
            return _rabbits.OrderByDescending(obj => obj.Name);
        }

        //public IEnumerable<Rabbit> SortByNameDescending() //LINQ (for SQL og C# folket)
        //{
        //    return from rabbit in _rabbits
        //           orderby rabbit.Name descending
        //           select rabbit;
        //}

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
        //END: ENDNU IKKE IMPLEMENTERET KODE. FORTSAT RELEVANT? :---- 


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
        /// <param name="breederRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, som er levende</returns>
        public List<Rabbit> GetOwnedAliveRabbits(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo && rabbit.DeadOrAlive == DeadOrAlive.Levende).ToList();
        }

        /// <summary>
        /// Funktion som tilgåes via knappen "Filter Kaniner" på GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i brugerens avler-ID og kæder det sammen med de kaniner,
        /// avleren ejer, som er døde.
        /// </summary>
        /// <param name="breederRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, som er døde</returns>
        public List<Rabbit> GetOwnedDeadRabbits(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo && rabbit.DeadOrAlive == DeadOrAlive.Død).ToList();
        }

        /// <summary>
        /// Funktion som tilgåes via knappen "Filter Kaniner" på GetAllRabbits siden.
        /// 
        /// Tager udgangspunkt i brugerens avler-ID og kæder det sammen med de kaniner,
        /// avleren ejer ELLER har forbindelse til (kaniner med avlerens ID)
        /// </summary>
        /// <param name="breederRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af avlerens ejede kaniner, og kaniner med avlerens ID</returns>
        public List<Rabbit> GetAllRabbitsWithConnectionsToMe(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner == breederRegNo || rabbit.OriginRegNo == breederRegNo).ToList();
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
        /// <param name="breederRegNo">Brugerens avler-ID</param>
        /// <returns>En liste af kaniner, som avleren IKKE ejer, men er rigistreret med avlerens avler-ID</returns>
        public List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int breederRegNo)
        {
            return _rabbits.Where(rabbit => rabbit.Owner != breederRegNo && rabbit.OriginRegNo == breederRegNo).ToList();
        }


    }
}
