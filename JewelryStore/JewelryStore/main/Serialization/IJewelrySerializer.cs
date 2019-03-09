using JewelryOop;
using System.Collections.Generic;
using System.IO;

namespace JewelryStore.main.Serialization {
    public interface IJewelrySerializer {
        void Serialize(object value, FileStream destination);

        List<Jewelry> Deserialize(FileStream source);
    }
}