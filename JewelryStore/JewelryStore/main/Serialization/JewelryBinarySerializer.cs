using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using JewelryOop;

namespace JewelryStore.main.Serialization {
    public class JewelryBinarySerializer : IJewelrySerializer {
        public JewelryBinarySerializer()
        {
            formatter = new BinaryFormatter();
        }

        private BinaryFormatter formatter { get; set; }

        public object Deserialize(Type t, FileStream source)
        {
            try
            {
                return formatter.Deserialize(source);
            }
            finally
            {
                source.Close();
            }
        }
        
        public string Serialize(object value)
        {
            var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, value);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
