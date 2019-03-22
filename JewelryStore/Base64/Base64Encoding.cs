using JewelryStore.main.Plugins;
using System;
using System.IO;
using System.Text;

namespace JewelryPlugins {
    public class Base64Encoding : IJewelryEncodingPlugin {
        public Base64Encoding()
        {
            Extension = DefaultValues.Base64EncodingExtension;
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

            var decoded = Convert.FromBase64String(decodedString);

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
            string newValue = Convert.ToBase64String(value);

            var result = new FileStream(fileName + Extension, FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(newValue);
            }

            result.Close();
        }

        public override string ToString()
        {
            return "Base64 Encoding";
        }
    }
}
