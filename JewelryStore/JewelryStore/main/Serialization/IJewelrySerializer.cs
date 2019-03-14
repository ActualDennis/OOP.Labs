using JewelryOop;
using System;
using System.Collections.Generic;
using System.IO;

namespace JewelryStore.main.Serialization {
    public interface IJewelrySerializer {
        void Serialize(object value, FileStream destination);

        object Deserialize(Type objectType, FileStream source);
    }
}