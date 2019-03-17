using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Plugins {
    public interface IJewelryEncodingPlugin {
        void Encode(string value, string fileName);

        /// <summary>
        /// Decodes contents of filestream and returns name of a file with decoded contents,
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string Decode(FileStream source, string fileName);

        string Extension { get; }
    }
}
