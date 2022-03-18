using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneXamarin.Service
{
    public class Phone : Entity
    {
        public string Type { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Camera { get; set; }
        public string Processor { get; set; }
        public string ScreenResolution { get; set; }
        public double Discount { get; set; }
        public int DiscountType { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; } = false;
        public string Image { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
