using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
	public class MockWool
	{
		private static List<Wool> _wools = new List<Wool>()
		{
			new Wool("Wool","Silke Uld", 50,10,"Grey", 1423, 13, 150.5),
            new Wool("Wool", "Sokke Uld", 50,10,"hvid", 1355, 10, 158.5)
        };

		public static List<Wool> GetMockWools() { return _wools; }
	}
}
