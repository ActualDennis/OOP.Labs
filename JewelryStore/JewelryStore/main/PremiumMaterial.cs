using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {
    [Serializable]
    [XmlType(TypeName = "ПремиальныйМатериал")]
    [XmlInclude(typeof(PremiumGemstone))]
    [TextClass]
    public class PremiumMaterial : Material {
        public PremiumMaterial(string name, double pricePerGram, double grams) : base(name, pricePerGram, grams)
        {
        }

        public PremiumMaterial()
        {

        }

        [NonSerialized]
        private const double GramsToCaratRatio = 0.2;

        public override string GetDescription()
        {
            return base.GetDescription() 
                + Environment.NewLine + 
                $"Carat ratio of this premium material is: {GramsToCaratRatio * Grams}";
        }
    }
}
