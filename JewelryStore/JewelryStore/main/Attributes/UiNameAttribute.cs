using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Attributes {
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class UiNameAttribute : Attribute {

        // This is a positional argument
        public UiNameAttribute()
        {
           
        }

        // This is a named argument
        public string Name { get; set; }
    }

}
