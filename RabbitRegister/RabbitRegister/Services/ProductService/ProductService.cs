using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public class ProductService : IProductService
    {
        private List<Wool> _wools;
        private List<Yarn> _yarns;

        private DbGenericService<Wool> _dbService;
        private DbGenericService<Yarn> _dbYarnService;

        public ProductService(DbGenericService<Wool> dbService, DbGenericService<Yarn> dbYarnService)
        {
            _dbService = dbService;
            _dbYarnService = dbYarnService;

            _wools = _dbService.GetObjectsAsync().Result.ToList();
            _yarns = dbYarnService.GetObjectsAsync().Result.ToList();

            // Using mock data for debugging purposes
            //_wools = MockWool.GetMockWools(); // Fetch mock wools data (replace with database query)
            //foreach (var wool in _wools)
            //{
            //    dbService.AddObjectAsync(wool).Wait(); // Add each wool object to the database
            //}
            //_yarns = MockYarn.GetMockYarns(); // Fetch mock yarns data (replace with database query)
            //foreach (var yarn in _yarns)
            //{
            //    dbYarnService.AddObjectAsync(yarn).Wait(); // Add each yarn object to the database
            //}
        }

        /// <summary>
        /// Retrieves a product based on the specified product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>The list of products.</returns>
        public List<Product> GetProduct(int productId)
        {
            GetWools(productId); // Retrieve wools associated with the product ID
            GetYarn(productId); // Retrieve yarns associated with the product ID
            return null; // TODO: Replace with actual implementation
        }

        /// <summary>
        /// Adds a new wool asynchronously.
        /// </summary>
        /// <param name="wool">The wool to add.</param>
        public async Task AddWoolAsync(Wool wool)
        {
            await _dbService.AddObjectAsync(wool); // Add wool object to the database
            _wools.Add(wool); // Add wool object to the local list
        }

        /// <summary>
        /// Updates an existing wool asynchronously.
        /// </summary>
        /// <param name="wool">The updated wool.</param>
        public async Task UpdateWoolAsync(Wool wool)
        {
            if (wool != null)
            {
                foreach (Wool w in _wools)
                {
                    if (w.ProductId == wool.ProductId && w.ProductType == wool.ProductType)
                    {
                        // Update wool properties
                        w.ProductName = wool.ProductName;
                        w.Weight = wool.Weight;
                        w.Quality = wool.Quality;
                        w.Color = wool.Color;
                        w.BreederRegNo = wool.BreederRegNo;
                        w.Amount = wool.Amount;
                        w.Price = wool.Price;
                        w.ImgString = wool.ImgString;
                        break;
                    }
                }
                await _dbService.UpdateObjectAsync(wool); // Update wool object in the database
            }
        }

        /// <summary>
        /// Deletes a wool asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the wool to delete.</param>
        /// <returns>The deleted wool.</returns>
        public async Task<Wool> DeleteWoolAsync(int? Id)
        {
            Wool woolToBeDeleted = null;
            foreach (Wool w in _wools)
            {
                if (w.ProductId == Id)
                {
                    woolToBeDeleted = w;
                    break;
                }
            }
            if (woolToBeDeleted != null)
            {
                _wools.Remove(woolToBeDeleted); // Remove wool object from the local list
                await _dbService.DeleteObjectAsync(woolToBeDeleted); // Delete wool object from the database
            }
            return woolToBeDeleted;
        }

        /// <summary>
        /// Retrieves all wools.
        /// </summary>
        /// <returns>The list of wools.</returns>
        public List<Wool> GetWools() { return _wools; }

        /// <summary>
        /// Retrieves the wools created by a specific breeder.
        /// </summary>
        /// <param name="breederRegNo">The registration number of the breeder.</param>
        /// <returns>The list of wools created by the breeder.</returns>
        public List<Wool> GetMyWoolCreations(int breederRegNo)
        {
            return _wools.Where(wool => wool.BreederRegNo == breederRegNo).ToList(); // Filter wools based on breeder registration number
        }

        /// <summary>
        /// Retrieves the yarns created by a specific breeder.
        /// </summary>
        /// <param name="breederRegNo">The registration number of the breeder.</param>
        /// <returns>The list of yarns created by the breeder.</returns>
        public List<Yarn> GetMyYarnCreations(int breederRegNo)
        {
            return _yarns.Where(yarn => yarn.BreederRegNo == breederRegNo).ToList(); // Filter yarns based on breeder registration number
        }

        /// <summary>
        /// Retrieves a specific wool based on the ID.
        /// </summary>
        /// <param name="id">The ID of the wool.</param>
        /// <returns>The wool object.</returns>
        public Wool GetWools(int id)
        {
            foreach (Wool w in _wools)
            {
                if (w.ProductId == id)
                    return w;
            }
            return null;
        }

        /// <summary>
        /// Retrieves all yarns.
        /// </summary>
        /// <returns>The list of yarns.</returns>
        public List<Yarn> GetYarns() { return _yarns; }

        /// <summary>
        /// Adds a new yarn asynchronously.
        /// </summary>
        /// <param name="yarn">The yarn to add.</param>
        public async Task AddYarnAsync(Yarn yarn)
        {
            await _dbYarnService.AddObjectAsync(yarn); // Add yarn object to the database
            _yarns.Add(yarn); // Add yarn object to the local list
        }

        /// <summary>
        /// Deletes a yarn asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the yarn to delete.</param>
        /// <returns>The deleted yarn.</returns>
        public async Task<Yarn> DeleteYarnAsync(int? Id)
        {
            Yarn yarnToBeDeleted = null;
            foreach (Yarn yarn in _yarns)
            {
                if (yarn.ProductId == Id)
                {
                    yarnToBeDeleted = yarn;
                    break;
                }
            }

            if (yarnToBeDeleted != null)
            {
                _yarns.Remove(yarnToBeDeleted); // Remove yarn object from the local list
                await _dbYarnService.DeleteObjectAsync(yarnToBeDeleted); // Delete yarn object from the database
            }
            return yarnToBeDeleted;
        }

        /// <summary>
        /// Retrieves a specific yarn based on the ID.
        /// </summary>
        /// <param name="Id">The ID of the yarn.</param>
        /// <returns>The yarn object.</returns>
        public Yarn GetYarn(int Id)
        {
            foreach (Yarn yarn in _yarns)
            {
                if (yarn.ProductId == Id)
                    return yarn;
            }
            return null;
        }

        /// <summary>
        /// Updates an existing yarn asynchronously.
        /// </summary>
        /// <param name="yarn">The updated yarn.</param>
        /// <param name="id">The ID of the yarn.</param>
        public void UpdateYarnAsync(Yarn yarn, int id)
        {
            if (yarn != null)
            {
                foreach (Yarn i in _yarns)
                {
                    if (i.ProductId == yarn.ProductId && i.ProductType == yarn.ProductType)
                    {
                        // Update yarn properties
                        i.BreederRegNo = yarn.BreederRegNo;
                        i.ProductName = yarn.ProductName;
                        i.Fiber = yarn.Fiber;
                        i.NeedleSize = yarn.NeedleSize;
                        i.Length = yarn.Length;
                        i.Tension = yarn.Tension;
                        i.Washing = yarn.Washing;
                        i.Amount = yarn.Amount;
                        i.ImgString = yarn.ImgString;
                        i.Color = yarn.Color;
                        i.Price = yarn.Price;
                        break;
                    }
                }
                _dbYarnService.UpdateObjectAsync(yarn); // Update yarn object in the database
            }
        }
    }
}
