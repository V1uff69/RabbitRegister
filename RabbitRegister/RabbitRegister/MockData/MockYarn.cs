using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
    public class MockYarn
    {
        private static List<Yarn> _Yarns = new List<Yarn>()
        {
            new Yarn("Yarn", 5095, "Ida's Garn", "Får", 3, 110, "MAX", "Håndvask", "Creme", 5, 249, null),
            new Yarn("Yarn", 5095, "Ida's Garn", "Angora45% & Moher55%", 4, 100, "MEDIUM", "Ca 40 grader", "Beige", 6, 299, null),
            new Yarn("Yarn", 5095, "Ida's Garn", "Angora", 2, 80, "LOW", "Ca 30 grader", "Hvid", 8, 199, null),
            new Yarn("Yarn", 5053, "Maja's Garn", "Angora", 3, 80, "LOW", "Ca 30 grader", "Hvid", 6, 225, null),
            new Yarn("Yarn", 5053, "Maja's Garn", "Satin Angora", 3, 90, "MEDIUM", "30 grader", "Brun", 7, 250, null)
        };

        public static List<Yarn> GetMockYarns() { return _Yarns; }
    }
}
