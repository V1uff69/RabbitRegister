using System;

namespace RabbitRegister.Model
{
    public class Yarn : Product
    {
        public int YarnId { get; set; }
        public string Fiber { get; set; }
        public double NeedleSize { get; set; }
        public double Length { get; set; }
        public string Tension { get; set; }
        public string Washing { get; set; }

        public Yarn()
        {
        }

        public Yarn(int productId, int breederRegNo, int yarnId, string productName, string fiber, double needleSize, double length, string tension, string washing, string color, int amount) : base(productId, breederRegNo, productName, color, amount)
        {
            YarnId = yarnId;
            Fiber = fiber;
            NeedleSize = needleSize;
            Length = length;
            Tension = tension;
            Washing = washing;
        }

    }
}
