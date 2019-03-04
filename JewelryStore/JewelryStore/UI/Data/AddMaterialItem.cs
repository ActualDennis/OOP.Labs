using JewelryStore.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore {
    public class EditJewelryItem {
        public string UiName { get; set; }

        public string Name { get; set; }

        public UiElementType ElementType { get; set; }

        public string[] EnumTypes { get; set; }

        /// <summary>
        /// UI value for users' chosen element from dropdown list
        /// </summary>

        public string ChosenEnumType { get; set; }

        /// <summary>
        /// Value for specified field; provided by user
        /// </summary>

        public string Value { get; set; }

        /// <summary>
        /// Type of field i.e Color
        /// </summary>

        public Type FieldType { get; set; }
    }
}
