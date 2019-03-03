using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class Bijouterie : Jewelry {
        public Bijouterie(string name, List<Material> materials, double foolRatio) : base(name, materials)
        {
            FoolRatio = foolRatio;
        }

        /// <summary>
        /// Determines if Bijouterie thing looks cheap or expensive?
        /// </summary>
        public double FoolRatio { get; set; }

        public override double GetPrice()
        {
            return base.GetPrice() * FoolRatio;
        }

        public override double GetRealPrice()
        {
            return base.GetRealPrice();
        }
    }
}
