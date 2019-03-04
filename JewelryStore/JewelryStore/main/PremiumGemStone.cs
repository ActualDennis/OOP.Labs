using JewelryStore.main.Attributes;
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
        [UiName(Name = "Цвет камня")]
        public Color GemColor { get; set; }
        [UiName(Name = "Уникальность (любое число)")]
        public double UniquenessRatio { get; set; }

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
