using JewelryOop;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JewelryStore.main.Serialization {
    [Serializable]
    [XmlRoot(ElementName = "Украшения")]
    public class JewelrySerialized {
        [XmlArray(ElementName = "Украшения")]
        public List<Jewelry> jewelries;
    }
}
