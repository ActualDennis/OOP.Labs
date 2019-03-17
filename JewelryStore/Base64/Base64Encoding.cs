using JewelryStore.main.Plugins;
using System;
using System.IO;
using System.Text;

namespace Base64 {
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

            decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(decodedString));

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
            value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

            var result = new FileStream(fileName + Extension, FileMode.Create);

            using (var writer = new StreamWriter(result))
            {
                writer.Write(value);
            }

            result.Close();
        }

        public override string ToString()
        {
            return "Base64 Encoding";
        }
    }
}
