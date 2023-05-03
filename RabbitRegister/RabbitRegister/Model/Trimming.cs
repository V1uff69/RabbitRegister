namespace RabbitRegister.Model
{
    public class Trimming
    {
        public Trimming()
        {
        }

        public Trimming(int trimmingId, int rabbitId, DateTime date, double minuttesUsed, double hairLengthByDayNinety, string woolDensity, double firstSortmentWeight, double secondSotrmentWeight, double disposeableWoolWeight)
        {
            TrimmingId = trimmingId;
            RabbitId = rabbitId;
            Date = date;
            MinuttesUsed = minuttesUsed;
            HairLengthByDayNinety = hairLengthByDayNinety;
            WoolDensity = woolDensity;
            FirstSortmentWeight = firstSortmentWeight;
            SecondSotrmentWeight = secondSotrmentWeight;
            DisposeableWoolWeight = disposeableWoolWeight;
        }

        public int TrimmingId { get; set; }
        public int RabbitId { get; set; }
        public DateTime Date { get; set; }
        public double MinuttesUsed { get; set; }
        public double HairLengthByDayNinety { get; set; }
        public string WoolDensity { get; set; }
        public double FirstSortmentWeight { get; set; }
        public double SecondSotrmentWeight  { get; set; }
        public double DisposeableWoolWeight { get; set; }


    }
}
