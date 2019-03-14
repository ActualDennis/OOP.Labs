using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Extensions {
    public static class StringExtensions {
        public static string GetValueInBetween(this string value, string firstSubString, string secondSubString, bool IsArray)
        {
            if (IsArray)
                return value.Substring(value.IndexOf(firstSubString) + 1,
                value.LastIndexOf(secondSubString) - value.IndexOf(firstSubString) - 1 - 1);

            return value.Substring(value.IndexOf(firstSubString) + 1,
                value.IndexOf(secondSubString) - value.IndexOf(firstSubString) - 1 - 1);
        }
    }
}
