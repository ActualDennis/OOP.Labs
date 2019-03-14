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

        public void Serialize(object value, FileStream destination)
        {
            try
            {
                formatter.Serialize(destination, value);
            }
            finally
            {
                destination.Close();
            }

        }
    }
}
