using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryOop {
    public class Jewelry {
        public Jewelry(string name, List<Material> materials)
        { 
            Materials = materials;
            Name = name;
        }

        public string Name { get; private set; }

        public List<Material> Materials { get; private set; }

        public virtual double GetPrice()
        {
            double result = 0;
            foreach (var material in Materials)
            {
                result += material.GetPrice();
            }

            return result;
        }

        public virtual double GetRealPrice()
        {
            double result = 0;
            foreach (var material in Materials)
            {
                result += material.GetPrice();
            }

            return result;
        }

    }
}
