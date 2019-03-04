using JewelryStore.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JewelryStore {
    public class EnumOrFieldTemplateSelector : DataTemplateSelector {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elem = container as FrameworkElement;

            var entry = (EditJewelryItem)item;

            if(entry.ElementType == UiElementType.Enum)
            {
                return elem.FindResource("EnumDataTemplate") as DataTemplate;
            }

            if (entry.ElementType == UiElementType.Field)
            {
                return elem.FindResource("FieldDataTemplate") as DataTemplate;
            }

            throw new ApplicationException();
        }
    }
}
