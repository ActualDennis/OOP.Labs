using JewelryOop;
using JewelryStore.main.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tests {
    public class Tests {
        Bijouterie bijouterie;
        Jewelry anotherJewelry;
        object x2;
        [SetUp]
        public void Setup()
        {
            var list = new List<Material>();
            list.Add(new Gemstone("gem", 12, 12, Color.Black));
            list.Add(new PremiumMaterial("premMat", 12, 12));
            list.Add(new Material("material", 12, 12));
            bijouterie = new Bijouterie("1", list, 1);
            anotherJewelry = new Jewelry("2", list);
        }

        [Test]
        public void BasicTest()
        {
            var y = new JewelrySerialized();
            y.jewelries = new List<Jewelry>()
            {
                bijouterie,
                anotherJewelry
            };

            var fs = new FileStream("H:/test.txt", FileMode.Create);

            var x = new TextSerializer();
          //  x.Serialize(y, fs);
        }

        public Type test1(object value)
        {
            return value.GetType();
        }
    }
}