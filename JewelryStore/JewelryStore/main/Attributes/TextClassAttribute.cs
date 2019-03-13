using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Attributes {
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class TextClassAttribute : Attribute {
    }
}
