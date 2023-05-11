using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
	public class MockYarn
	{
		private static List<Yarn> _Yarns = new List<Yarn>()
		{
			new Yarn("1", 1, "GoldYarn", "Double", 1.2, 11, "MAX", "Ja", "SnowWhite", 20, 249),
			new Yarn("2", 2, "PlatYarn", "Single", 1.4, 22, "MEDIUM", "Nej", "CandyRed", 25, 299),
			new Yarn("3", 3, "SilverYarn", "Trible", 0.9, 33, "LOW", "Ja", "HighPurple", 15, 199)
		};

		public static List<Yarn> GetMockYarns() { return _Yarns; }
	}
}
