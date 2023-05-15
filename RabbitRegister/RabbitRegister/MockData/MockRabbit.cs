using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
    public class MockRabbit
    {
        private static List<Rabbit> _rabbits = new List<Rabbit>()
        {
            new Rabbit(
                1,
                5095,
                "Kaliba",
                "Angora",
                "Grå",
                Sex.Female,
                new DateTime(2019, 02, 27),
                4,
                8,
                DeadOrAlive.Alive,
                IsForSale.No,
                "Meget god avlskanin og god mor. Mange gode egenskaber såsom tæt pels, og rolige tendenser ved klip.",
                null),

            new Rabbit(
                105,
                4640,
                "Ingolf",
                "Engelsk Angora",
                "Blå Chinchilla",
                Sex.Male,
                new DateTime(2021, 04, 05),
                2,
                5,
                DeadOrAlive.Alive,
                IsForSale.No,
                "Han er ret lille i forhold til hvad standarden siger. Meget imødekommende",
                null),

            new Rabbit(
                2,
                5095,
                "Sov",
                "Angora",
                "Sort",
                Sex.Female,
                new DateTime(2020, 06, 12),
                3,
                7,
                DeadOrAlive.Dead,
                IsForSale.No,
                "Meget sky kanin. Havde ikke gode egenskaber for at blive tam, men tilgengæld gode pels kvaliteter",
                "Fødselskomplikationer - mulighvis for langt imellem parring"),

            new Rabbit(
                3,
                5095,
                "Smørklat smør",
                "Fransk Angora",
                "Gul",
                Sex.Female,
                new DateTime(2020, 03, 12),
                5,
                8,
                DeadOrAlive.Dead,
                IsForSale.No,
                "Hun filtrer meget i nakken og på maven, ellers hurtigvoksende pels",
                null),

             new Rabbit(
                67,
                4982,
                "Frida",
                "Satin Angora",
                "Ræve Rød",
                Sex.Female,
                new DateTime(2022, 06, 22),
                3,
                7,
                DeadOrAlive.Alive,
                IsForSale.No,
                "Hun er til låns.. Hun er bliver meget let stresset og sky",
                null),

             new Rabbit(
                120,
                4640,
                "Mulan",
                "Angora",
                "Blå",
                Sex.Female,
                new DateTime(2021, 05, 11),
                4,
                6,
                DeadOrAlive.Alive,
                IsForSale.No,
                null,
                null)
        };

        public static List<Rabbit> GetMockRabbits()
        { return _rabbits; }

    }
}
