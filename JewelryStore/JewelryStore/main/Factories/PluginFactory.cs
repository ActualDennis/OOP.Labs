using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Plugins {
    public static class PluginFactory {
        public static IJewelryEncodingPlugin GetPlugin(string fileExtension, List<IJewelryEncodingPlugin> plugins)
        {
            return plugins.Find(x => x.Extension == fileExtension);
        }
    }
}
