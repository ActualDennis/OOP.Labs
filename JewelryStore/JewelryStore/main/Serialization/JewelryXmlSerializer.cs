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
        public object Deserialize(Type objectType, FileStream source)
        {
            try
            {
                var serializer = new XmlSerializer(objectType);

                return serializer.Deserialize(source);
            }
            finally
            {
                source.Close();
            }
        }

        public byte[] Serialize(object value)
        {
            var serializer = new XmlSerializer(value.GetType());

            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                writer.WriteProcessingInstruction("xml", "version='1.0'");
                serializer.Serialize(writer, value);

                return Encoding.UTF8.GetBytes(stringWriter.ToString());
            }
        }
    }
}
