using JewelryOop;
using JewelryStore.main.Attributes;
using JewelryStore.UI.Data;
using JewelryStore.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelryStore.UI.ViewModels {
    public class AddMaterialsViewModel : BaseViewModel {
       
        public ObservableCollection<EditJewelryItem> Fields { get; set; }

        private Material WhatToAdd { get; set; }

        public ICommand AlterMaterialCommand => new RelayCommand(() => AlterMaterial(null), null);

        private Action Callback { get; set; }

        /// <summary>
        /// Method used to update fields of object that will be edited on the ui
        /// </summary>
        /// <param name="item"></param>
        public void AlterMaterial(ref Material item, Action callback)
        {
            Callback = callback;
            Fields = null;
            Fields = new ObservableCollection<EditJewelryItem>();
            var props = item.GetType().GetProperties();

            foreach(var property in props)
            {
                if (property.GetMethod.ReturnType.IsEnum)
                {
                    Fields.Add(new EditJewelryItem()
                    {
                        UiName = property.GetCustomAttribute<UiNameAttribute>(false).Name,
                        Name = property.Name,
                        ElementType = UiElementType.Enum,
                        EnumTypes = property.GetMethod.ReturnType.GetEnumNames(),
                        FieldType = property.GetMethod.ReturnType,
                    });
                }
                else
                {
                    Fields.Add(new EditJewelryItem() { Name = property.Name, UiName = property.GetCustomAttribute<UiNameAttribute>(false).Name, ElementType = UiElementType.Field, FieldType = property.GetMethod.ReturnType});
                }
            }

            WhatToAdd = item;
        }
               
        private void AlterMaterial(Object parameter)
        {
            try
            {
                Type type = WhatToAdd.GetType();
                PropertyInfo[] fields = type.GetProperties();

                foreach (var field in fields)
                {
                    foreach (var alteredField in Fields)
                    {
                        if (alteredField.Name == field.Name)
                        {
                            //if this is enum, choose appropriate field and set it to object

                            if (alteredField.EnumTypes != null && alteredField.EnumTypes.Length != 0)
                            {
                                field.SetValue(WhatToAdd, Convert.ChangeType(GetEnumMemberValueByName(alteredField.ChosenEnumType, alteredField.FieldType), alteredField.FieldType));
                            }
                            else
                            {
                                if (field.GetType().IsValueType)
                                {
                                    field.SetValue(WhatToAdd, Convert.ChangeType(double.Parse(alteredField.Value), alteredField.FieldType));
                                }
                                else
                                {
                                    field.SetValue(WhatToAdd, Convert.ChangeType(alteredField.Value, alteredField.FieldType));
                                }
                            }

                            break;
                        }
                    }
                }

                Callback.Invoke();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Wrong data was provided. Can't edit that material. Detailed Error: {ex.Message}");
            }
        }

        public object GetEnumMemberValueByName(string fieldName, Type enumType)
        {
            return Enum.Parse(enumType, fieldName);
        }
    }
}
