using JewelryOop;
using System;
using System.Collections.Generic;
using System.IO;

namespace JewelryStore.main.Serialization {
    public interface IJewelrySerializer {
        byte[] Serialize(object value);

        object Deserialize(Type objectType, FileStream source);
    }
}