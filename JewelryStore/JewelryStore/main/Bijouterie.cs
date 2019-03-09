using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {

    [Serializable]
    [XmlRoot(ElementName = "Бижутерия")]
    public class Bijouterie : Jewelry {
        public Bijouterie(string name, List<Material> materials, double foolRatio) : base(name, materials)
        {
            FoolRatio = foolRatio;
        }

        private Bijouterie() : base()
        {

        }

        /// <summary>
        /// Determines if Bijouterie thing looks cheap or expensive?
        /// </summary>
        [XmlAttribute(AttributeName = "Новизна")]
        [UiName(Name = "Насколько хорошо ( % ) выглядит украшение")]
        public double FoolRatio { get; set; }

        public override double GetPrice()
        {
            return base.GetPrice() * FoolRatio;
        }

        public override double GetRealPrice()
        {
            return base.GetRealPrice();
        }

        public override string GetDescription() => base.GetDescription();

        public override string ToString() => base.ToString();
    }
}
