using JewelryStore.main.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryPlugins {
    public class Base32Encoding : IJewelryEncodingPlugin {
        public Base32Encoding()
        {
            Extension = DefaultValues.Base32EncodingExtension;
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

            decodedString = Encoding.UTF8.GetString(Wiry.Base32.Base32Encoding.Standard.ToBytes(decodedString));

            var result = new FileStream(fileName + "1", FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(decodedString);
            }

            result.Close();

            return fileName + "1";
        }

        public void Encode(string value, string fileName)
        {

            value = Wiry.Base32.Base32Encoding.Standard.GetString(Encoding.UTF8.GetBytes(value));

            var result = new FileStream(fileName + Extension, FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(value);
            }

            result.Close();
        }

        public override string ToString()
        {
            return "Base32 Encoding";
        }
    }
}
