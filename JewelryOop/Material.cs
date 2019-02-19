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
        public string Name { get; private set; }

        public double PricePerGram { get; private set; }

        public double Grams { get; private set; }

        public virtual double GetPrice()
        {
            return Grams * PricePerGram;
        }

        public virtual string GetDescription()
        {
            return$"Name : {Name} Weight : {Grams} Price: {GetPrice()}";
        }
    }
}
