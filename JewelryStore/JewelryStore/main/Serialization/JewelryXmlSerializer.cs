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
    public static class JewelryXmlSerializer {
        public static string SerializeIndented(object value)
        {
            var objToSerialize = new JewelrySerialized();

            objToSerialize.jewelries = (List<Jewelry>) value;

            var serializer = new XmlSerializer(objToSerialize.GetType());
            
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                serializer.Serialize(writer, objToSerialize);
                return stringWriter.ToString();
            }
        }
    }
}
