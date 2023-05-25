using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
	public class MockWool
	{
		private static List<Wool> _wools = new List<Wool>()
		{
			new Wool("Wool","Silke Uld", 50, 1,"Grey", 5095, 13, 150.5, null),
            new Wool("Wool", "Sokke Uld", 80, 2,"Creme", 5095, 9, 120, null),
            new Wool("Wool", "Maja's Batch1", 125, 1,"Creme", 5053, 5, 150, null),
            new Wool("Wool", "Maja's Batch2", 140, 2,"Creme", 5053, 5, 140, null),
            new Wool("Wool", "Angora", 115, 1,"Grå", 5095, 6, 160.5, null),
            new Wool("Wool", "Satin Angora", 210, 2,"Blå", 5095, 7, 185.5, null)
        };

        public static List<Wool> GetMockWools() { return _wools; }
    }
}
