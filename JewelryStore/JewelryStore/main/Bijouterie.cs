﻿using JewelryStore.main;
using JewelryStore.main.Attributes;
using JewelryStore.main.Visitors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryOop {

    [Serializable]
    [TextClass]
    [XmlRoot(ElementName = "Бижутерия")]
    public class Bijouterie : Jewelry, IJewelry {
        public Bijouterie(string name, List<Material> materials, double foolRatio) : base(name, materials)
        {
            FoolRatio = foolRatio;
        }

        public Bijouterie() : base()
        {

        }

        /// <summary>
        /// Determines if Bijouterie thing looks cheap or expensive?
        /// </summary>
        [XmlAttribute(AttributeName = "Новизна")]
        [TextField]
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
        public override void Accept(IJewelryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
