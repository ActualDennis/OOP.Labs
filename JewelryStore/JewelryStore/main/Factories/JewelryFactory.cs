using JewelryOop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main {
    public class JewelryFactory {
        public Jewelry NewJewelry(string jewelryName)
        {
            switch (jewelryName)
            {
                case "Jewelry":
                    return new Jewelry();
                case "Bijouterie":
                    return new Bijouterie();
                default:
                        return new Jewelry();
            }
        }
    }
}
