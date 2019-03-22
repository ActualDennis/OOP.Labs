using Base58Check;
using JewelryStore.main.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryPlugins {
    public class Base58Encoding : IJewelryEncodingPlugin {
        public Base58Encoding()
        {
            Extension = DefaultValues.Base58EncodingExtension;
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

            var decoded = Base58CheckEncoding.DecodePlain(decodedString);

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

            string newValue = Base58CheckEncoding.EncodePlain(value);
            
            var result = new FileStream(fileName + Extension, FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(newValue);
            }

            result.Close();
        }

        public override string ToString()
        {
            return "Base58 Encoding";
        }
    }
}
