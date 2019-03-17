using JewelryStore.main.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Factories {
    public class JewelrySerializersFactory {
        public IJewelrySerializer NewSerializer(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName).TrimStart('.');

            if (fileExtension.StartsWith("xml"))
                return new JewelryXmlSerializer();

            if (fileExtension.StartsWith("bin"))
                return new JewelryBinarySerializer();

            if (fileExtension.StartsWith("txt"))
                return new TextSerializer();

            return null;
        }
    }
}
