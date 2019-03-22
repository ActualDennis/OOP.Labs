using JewelryPlugins;
using JewelryStore.main.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryPlugins {
    public class ZBase32Encoding : IJewelryEncodingPlugin {
        public ZBase32Encoding()
        {
            Extension = DefaultValues.ZBase32EncodingExtension;
        }
        
        public string Extension { get; }

        public string Decode(FileStream source, string fileName)
        {
            string decodedString;

            using (var reader = new StreamReader(source))
            {
                decodedString = reader.ReadToEnd();
            }

            source.Close();

            var decoded = Wiry.Base32.Base32Encoding.ZBase32.ToBytes(decodedString);
             
            var result = new FileStream(fileName + "1", FileMode.Create);

            using (var writer = new BinaryWriter(result))
            {
                writer.Write(decoded);
            }

            result.Close();

            return fileName + "1";
        }

        public void Encode(byte[] value, string fileName)
        {
             
            string newValue = Wiry.Base32.Base32Encoding.ZBase32.GetString(value);

            var result = new FileStream(fileName + Extension, FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(newValue);
            }

            result.Close();
        }

        public override string ToString()
        {
            return "ZBase32 Encoding";
        }
    }
}
