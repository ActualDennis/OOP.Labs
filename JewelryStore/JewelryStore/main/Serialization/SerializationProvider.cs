using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Serialization {
    public class SerializationProvider {

        public SerializationProvider()
        {
            Serializers = new Dictionary<string, IJewelrySerializer>()
            {
                { ".xml", new JewelryXmlSerializer() },
                { ".bin", new JewelryBinarySerializer() }
            };

            //add some code here to add additional serializers from dlls
        }

        public Dictionary<string, IJewelrySerializer> Serializers { get; set; }

        public Dictionary<string, IJewelrySerializer> LoadedSerializers { get; set; }

        public IJewelrySerializer GetSerializer(string extension)
        {
            if (Serializers.ContainsKey(extension))
            {
                return Serializers[extension];
            }

            if (LoadedSerializers.ContainsKey(extension))
            {
                return LoadedSerializers[extension];
            }

            return null;
        }
    }
}
