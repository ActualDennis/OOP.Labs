using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {

    [Serializable]
    [XmlType(TypeName = "Камень")]
    public class Gemstone : Material {
        public Gemstone(
            string name, 
            double pricePerGram, 
            double grams, 
            Color gemColor) : base(name, pricePerGram, grams)
        {
            GemColor = gemColor;
        }

        private Gemstone()
        {

        }

        [XmlAttribute(AttributeName = "ЦветКамня")]
        [UiName(Name = "Цвет камня")]
        public Color GemColor { get; set; }

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
