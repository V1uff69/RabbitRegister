using System;

namespace RabbitRegister.Model
{
    public class Yarn : Product
    {
        public Yarn()
        {
        }

        public Yarn(int productId, string productName, string color, int amount) : base(productId, productName, color, amount)
        {
        }

        public Yarn(int yarnId, int breederRegNo, string fiber, double needleSize, double length, string tension, string washing)
        {
            YarnId = yarnId;
            BreederRegNo = breederRegNo;
            Fiber = fiber;
            NeedleSize = needleSize;
            Length = length;
            Tension = tension;
            Washing = washing;
        }

        public int YarnId { get; set; }
      public int BreederRegNo { get; set; }
      public string Fiber { get; set; }
      public double NeedleSize { get; set; }
      public double Length { get; set; }
      public string Tension { get; set; }
      public string Washing { get; set; }
    }
}
