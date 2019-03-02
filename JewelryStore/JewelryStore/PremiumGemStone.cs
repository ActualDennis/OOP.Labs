using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class PremiumGemstone : PremiumMaterial {
        public PremiumGemstone(
            string name, 
            double pricePerGram, 
            double grams, 
            Color GemColor, 
            double uniquenessRatio) : base(name, pricePerGram, grams)
        {
            this.GemColor = GemColor;
            this.UniquenessRatio = uniquenessRatio;
        }

        public Color GemColor { get; private set; }

        public double UniquenessRatio { get; private set; }

        public override double GetPrice()
        {
            return base.GetPrice() * UniquenessRatio;
        }

        public override string GetDescription()
        {
            return base.GetDescription() +
                Environment.NewLine +
                GetGemDescription();
        }

        protected string GetGemDescription()
        {
            return "Color of this gem is : "
                + GemColor.ToString();
        }
    }
}
