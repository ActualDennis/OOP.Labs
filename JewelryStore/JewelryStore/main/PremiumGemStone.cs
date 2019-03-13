using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {

    [Serializable]
    [TextClass]
    [XmlType(TypeName = "ДорогойКамень")]
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

        private PremiumGemstone()
        {

        }

        [TextField]
        [XmlAttribute(AttributeName = "ЦветКамня")]
        [UiName(Name = "Цвет камня")]
        public Color GemColor { get; set; }

        [TextField]
        [XmlAttribute(AttributeName = "Уникальность")]
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
