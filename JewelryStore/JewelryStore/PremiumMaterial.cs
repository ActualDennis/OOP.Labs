using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class PremiumMaterial : Material {
        public PremiumMaterial(string name, double pricePerGram, double grams) : base(name, pricePerGram, grams)
        {
        }

        private const double GramsToCaratRatio = 0.2;

        public override string GetDescription()
        {
            return base.GetDescription() 
                + Environment.NewLine + 
                $"Carat ratio of this premium material is: {GramsToCaratRatio * Grams}";
        }
    }
}
