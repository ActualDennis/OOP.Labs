using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Serialization {
    public static class SerializationHelper {
        public static bool IsPluginUsed(string fileExtension)
        {
            if (fileExtension == ".xml" || fileExtension == ".bin" || fileExtension == ".txt")
                return false;

            return true;
        }

        public static string GetPluginExtension(string fileExtension)
        {
            fileExtension = fileExtension.Replace(".txt", "");
            fileExtension = fileExtension.Replace(".bin", "");
            fileExtension = fileExtension.Replace(".xml", "");

            return fileExtension;
        }
    }
}
