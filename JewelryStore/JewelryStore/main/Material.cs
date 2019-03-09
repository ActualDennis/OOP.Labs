using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {

    [XmlInclude(typeof(PremiumMaterial))]
    [XmlInclude(typeof(Gemstone))]
    [Serializable]
    [XmlType(TypeName = "Материал")]
    public class Material {
        public Material(string name, double pricePerGram, double grams)
        {
            Name = name;
            PricePerGram = pricePerGram;
            Grams = grams;
        }

        public Material()
        {

        }


        [XmlAttribute(AttributeName = "Имя")]
        [UiName(Name = "Имя")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "ЦенаЗаГрамм")]
        [UiName(Name = "Цена за грам")]
        public double PricePerGram { get; set; }

        [XmlAttribute(AttributeName = "Кол-воГрамм")]
        [UiName(Name = "Кол-во грамм")]
        public double Grams { get; set; }

        public virtual double GetPrice()
        {
            return Grams * PricePerGram;
        }

        public virtual string GetDescription()
        {
            return$"Name : {Name} Weight : {Grams} Price: {GetPrice()}";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
