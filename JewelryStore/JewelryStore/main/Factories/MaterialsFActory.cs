using JewelryOop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Factories {
    public class MaterialsFactory {
        public Material NewMaterial(string name)
        {
            switch (name)
            {
                case "Gemstone":
                    return new Gemstone();
                case "Premium Gemstone":
                    return new PremiumGemstone();
                case "Premium Material":
                    return new PremiumMaterial();
                default:
                    return new Material();
            }
        }
    }
}
