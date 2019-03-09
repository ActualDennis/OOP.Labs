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

        public List<Jewelry> Deserialize(FileStream source)
        {
            try
            {
                var tempObj = (JewelrySerialized)formatter.Deserialize(source);
                return tempObj.jewelries;
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
                var tempObj = new JewelrySerialized();
                tempObj.jewelries = (List<Jewelry>)value;
                formatter.Serialize(destination, tempObj);
            }
            finally
            {
                destination.Close();
            }

        }
    }
}
