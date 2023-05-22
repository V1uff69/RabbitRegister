using RabbitRegister.Model;
using static System.Net.Mime.MediaTypeNames;

namespace RabbitRegister.MockData
{
    public class MockRabbit
    {
        private static List<Rabbit> _rabbits = new List<Rabbit>()
        {
            new Rabbit(
                1,
                5095,
                5095,
                "Kaliba",
                "Angora",
                "Grå",
                Sex.Hun,
                new DateTime(2019, 02, 27),
                4,
                92,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                "Meget god avlskanin og god mor. Mange gode egenskaber såsom tæt pels, og rolige tendenser ved klip.",
                null,
                "/Images/Rabbits/Kaliba.jpg"),

            new Rabbit(
                2,
                5095,
                5095,
                "Sov",
                "Angora",
                "Sort",
                Sex.Hun,
                new DateTime(2020, 06, 12),
                3,
                74,
                DeadOrAlive.Død,
                IsForSale.Nej,
                "Meget sky kanin. Havde ikke gode egenskaber for at blive tam, men tilgengæld gode pels kvaliteter",
                "Fødselskomplikationer - mulighvis for langt imellem parring",
                null),

            new Rabbit(
                3,
                5095,
                5095,
                "Smørklat smør",
                "Fransk Angora",
                "Gul",
                Sex.Hun,
                new DateTime(2020, 03, 12),
                5,
                75,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                "Hun filtrer meget i nakken og på maven, ellers hurtigvoksende pels",
                null,
                "/Images/Rabbits/Smoerklatsmoer.jpg"),

            new Rabbit(
                4,
                5095,
                5053,
                "Fiktiv",
                "Fransk Angora",
                "Hvid",
                Sex.Hun,
                new DateTime(2019, 09, 21),
                4,
                66,
                DeadOrAlive.Død,
                IsForSale.Nej,
                null,
                null,
                "/Images/Rabbits/Fiktiv.jpg"),

            new Rabbit(
                120,
                4640,
                5095,
                "Mulan",
                "Angora",
                "Blå",
                Sex.Hun,
                new DateTime(2021, 05, 11),
                4,
                87,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                null,
                null,
                "/Images/Rabbits/Mulan.jpg"),

            new Rabbit(
                105,
                4640,
                5095,
                "Ingolf",
                "Engelsk Angora",
                "Blå Chinchilla",
                Sex.Han,
                new DateTime(2021, 04, 05),
                2,
                89,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                "Han er ret lille i forhold til hvad standarden siger. Meget imødekommende",
                null,
                "/Images/Rabbits/Ingolf.jpg"),

             new Rabbit(
                67,
                4982,
                5053,
                "Frida",
                "Satin Angora",
                "Ræve Rød",
                Sex.Hun,
                new DateTime(2022, 06, 22),
                3,
                62,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                "Hun er til låns.. Hun er bliver meget let stresset og sky",
                null,
                "/Images/Rabbits/Frida.jpg"),

            new Rabbit(
                1,
                5053,
                5053,
                "Niko",
                "Satin Angora",
                "Vildt Rød",
                Sex.Han,
                new DateTime(2022, 10, 18),
                5,
                72,
                DeadOrAlive.Levende,
                IsForSale.Nej,
                "Ikke prangende, men den bedste Satin Angora der findes for nu",
                null,
                null)
        };



        public static List<Rabbit> GetMockRabbits()
        { return _rabbits; }

    }
}
