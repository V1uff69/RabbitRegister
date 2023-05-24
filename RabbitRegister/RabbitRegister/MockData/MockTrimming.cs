using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
    public class MockTrimming
    {
        private static List<Trimming> _trimmings = new List<Trimming>()
        {
            new Trimming(1, 5095, "Kaliba", new DateTime(2023, 02, 27), 60, 6, 18, 75, 95, 20),
            new Trimming(3, 5095, "Smørklat", new DateTime(2023, 02, 27), 75, 7, 19, 100, 40, 30),
            new Trimming(105, 4640, "Ingolf", new DateTime(2023, 04, 28), 90, 6, 14, 60, 60, 40),
            new Trimming(120, 4640, "Mulan", new DateTime(2023, 01, 29), 55, 5, 18, 65, 75, 20),
            new Trimming(1, 5095, "Kaliba", new DateTime(2022, 09, 10), 45, 6, 18, 90, 75, 20),
            new Trimming(1, 5095, "Kaliba", new DateTime(2023, 01, 02), 75, 7, 19, 95, 40, 40),
            new Trimming(1, 5095, "Kaliba", new DateTime(2022, 11, 15), 75, 6, 18, 55, 60, 60),
            new Trimming(120, 4640, "Mulan", new DateTime(2022, 11, 03), 65, 5, 18, 65, 40, 40),
            new Trimming(120, 4640, "Mulan", new DateTime(2022, 05, 11), 75, 6, 16, 45, 85, 20),
            new Trimming(120, 4640, "Mulan", new DateTime(2023, 04, 16), 55, 5, 18, 85, 70, 20),
        };

        public static List<Trimming> GetMockTrimming()
        { return _trimmings; }
    }
}
