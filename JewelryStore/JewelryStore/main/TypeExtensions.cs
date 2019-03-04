using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main {
    public static class TypeExtensions {
        public static bool IsGenericList(this Type o)
        {
            return (o.IsGenericType && (o.GetGenericTypeDefinition() == typeof(List<>)));
        }
    }
}
