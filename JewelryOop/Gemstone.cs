using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class Gemstone : Material {
        public Gemstone(
            string name, 
            double pricePerGram, 
            double grams, 
            Color gemColor) : base(name, pricePerGram, grams)
        {
            GemColor = gemColor;
        }

        public Color GemColor { get; private set; }

        public override double GetPrice()
        {
            return base.GetPrice();
        }

        public override string GetDescription()
        {
            return base.GetDescription() +
                Environment.NewLine
                + GetGemDescription();
        }

        protected string GetGemDescription()
        {
            return "Color of this gem is : "
                + GemColor.ToString();
        }

    }
}
