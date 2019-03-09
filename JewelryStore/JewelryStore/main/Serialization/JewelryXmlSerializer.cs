using JewelryOop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JewelryStore.main.Serialization {
    public class JewelryXmlSerializer : IJewelrySerializer {
        public List<Jewelry> Deserialize(FileStream source)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(JewelrySerialized));

                var tempObj = (JewelrySerialized)serializer.Deserialize(source);

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
                var objToSerialize = new JewelrySerialized();

                objToSerialize.jewelries = (List<Jewelry>)value;

                var serializer = new XmlSerializer(objToSerialize.GetType());

                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
                {
                    writer.WriteProcessingInstruction("xml", "version='1.0'");
                    serializer.Serialize(writer, objToSerialize);

                    using (var writer2 = new StreamWriter(destination))
                    {
                        writer2.Write(stringWriter.ToString());
                        writer2.Flush();
                    }
                }
            }
            finally
            {
                destination.Close();
            }
        }
    }
}
