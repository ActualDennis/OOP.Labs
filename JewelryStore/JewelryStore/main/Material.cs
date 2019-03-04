using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class Material {
        public Material(string name, double pricePerGram, double grams)
        {
            Name = name;
            PricePerGram = pricePerGram;
            Grams = grams;
        }

        [UiName(Name = "Имя")]
        public string Name { get; set; }

        [UiName(Name = "Цена за грам")]
        public double PricePerGram { get; set; }

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
