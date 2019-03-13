using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Attributes {
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class TextFieldAttribute : Attribute {
    }
}
