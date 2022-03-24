using System;

namespace PhoneXamarin.Service
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }

        public double TotalPrice { get; set; }

        public double VatPercentage { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Deleted { get; set; } = false;
        public int Reason { get; set; }
    }
}