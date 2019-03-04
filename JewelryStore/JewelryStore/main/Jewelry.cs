using JewelryStore.main.Attributes;
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

        [UiName(Name = "Имя")]
        public string Name { get; set; }

        private List<Material> materials;

        public List<Material> Materials
        {
            get => materials;
            set
            {
                if (value == null)
                    materials = new List<Material>();

                materials = value;
            }
        }

        public void AddMaterial(Material material)
        {
            materials.Add(material);
        }

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

        public virtual string GetDescription()
        {
            var result = string.Empty;
            result += $"Jewelry named: {Name} , look-like price: {GetPrice()}, priced: {GetRealPrice()}";
            result += $"{Environment.NewLine}Materials used:";
            int counter = 0;
            foreach (var material in Materials)
            {
                ++counter;
                result += $"{Environment.NewLine}{counter}. {material.GetDescription()}";
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
