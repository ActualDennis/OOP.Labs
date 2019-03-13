using JewelryOop;
using JewelryStore.main.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JewelryStore.main.Serialization {
    [TextClass]
    [Serializable]
    [XmlRoot(ElementName = "Украшения")]
    public class JewelrySerialized {
        public JewelrySerialized()
        {

        }

        [TextArray]
        [XmlArray(ElementName = "Украшения")]
        public List<Jewelry> jewelries { get; set; }
    }
}
