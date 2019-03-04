using JewelryOop;
using JewelryStore.main;
using JewelryStore.main.Attributes;
using JewelryStore.UI.Data;
using JewelryStore.UI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelryStore.UI.ViewModels {
    public class EditJewelryViewModel : BaseViewModel {
        public EditJewelryViewModel(AddMaterialsViewModel addMaterialsVm, Action MaterialEditedCallback, Action EditMaterialCallback)
        {
            this.addMaterialsVm = addMaterialsVm;
            this.MaterialEditedCallback = MaterialEditedCallback;
            this.EditMaterialCallback = EditMaterialCallback;
        }
       
        public ObservableCollection<EditJewelryItem> Fields { get; set; }

        public ICommand EditJewelryCommand => new RelayCommand(() => EditJewelry(null), null);

        public ICommand EditMaterialCommand => new RelayCommand(() => EditMaterial(null), null);

        public ICommand ViewInfoCommand => new RelayCommand(() => ViewInfo(null), null);

        public string JewelryDescription { get; set; }

        public bool IsEditingJewelryAllowed { get; set; }

        public bool IsEditingMaterialAllowed { get; set; }

        private Jewelry JewelryToEdit { get; set; }

        public string ChosenMaterial { get; set; }

        public List<Material> EditableJewelryMaterials { get; set; } 

        public AddMaterialsViewModel addMaterialsVm { get; set; }

        private Action MaterialEditedCallback { get; set; }

        public Action EditMaterialCallback { get; set; }
        /// <summary>
        /// Method used to update fields of object that will be edited on the ui
        /// </summary>
        /// <param name="item"></param>
        public void InitJewelryFields(ref Jewelry item)
        {
            Fields = null;
            Fields = new ObservableCollection<EditJewelryItem>();
            var props = item.GetType().GetProperties();

            foreach(var property in props)
            {
                //if this is list of materials, skip
                if (property.PropertyType.IsGenericList())
                    continue;

                if (property.GetMethod.ReturnType.IsEnum)
                {
                    Fields.Add(new EditJewelryItem()
                    {
                        UiName = property.GetCustomAttribute<UiNameAttribute>(false).Name,
                        Name = property.Name,
                        ElementType = UiElementType.Enum,
                        EnumTypes = property.GetMethod.ReturnType.GetEnumNames(),
                        FieldType = property.GetMethod.ReturnType
                    });
                }
                else
                {
                    Fields.Add(new EditJewelryItem() { Name = property.Name, UiName = property.GetCustomAttribute<UiNameAttribute>(false).Name, ElementType = UiElementType.Field, FieldType = property.GetMethod.ReturnType});
                }
            }

            JewelryToEdit = item;
            EditableJewelryMaterials = JewelryToEdit.Materials;
            IsEditingJewelryAllowed = true;
            IsEditingMaterialAllowed = true;
        }
               
        private void EditMaterial(Object parameter)
        {
            try
            {
                IsEditingMaterialAllowed = false;
                var editableMaterial = EditableJewelryMaterials.Find(x => x.ToString() == ChosenMaterial);
                var editableMaterialIndex = EditableJewelryMaterials.FindIndex(x => x.ToString() == ChosenMaterial);

                if (editableMaterial == null)
                {
                    MessageBox.Show($"Choose one of the materials from the dropdown menu.");
                    return;
                }
                    

                OnEditMaterial();

                addMaterialsVm.AlterMaterial(ref editableMaterial, () => OnMaterialEdited());

                EditableJewelryMaterials[editableMaterialIndex] = editableMaterial;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Wrong data was provided. Can't add that material. Detailed Error: {ex.Message}");
                OnMaterialEdited();
                IsEditingMaterialAllowed = true;
            }
        }

        private void OnMaterialEdited()
        {
            MaterialEditedCallback.Invoke();

            ChosenMaterial = null;

            IsEditingMaterialAllowed = true;
        }

        private void OnEditMaterial()
        {
            EditMaterialCallback.Invoke();
        }

        private void EditJewelry(Object parameter)
        {
            IsEditingJewelryAllowed = false;
            try
            {
                Type type = JewelryToEdit.GetType();
                PropertyInfo[] fields = type.GetProperties();

                foreach (var field in fields)
                {
                    if (field.GetMethod.ReturnType.IsGenericList())
                        continue;

                    foreach (var alteredField in Fields)
                    {
                        if (alteredField.Name == field.Name)
                        {
                            //if this is enum, choose appropriate field and set it to object

                            if (alteredField.EnumTypes != null && alteredField.EnumTypes.Length != 0)
                            {
                                field.SetValue(JewelryToEdit, Convert.ChangeType(GetEnumMemberValueByName(alteredField.ChosenEnumType, alteredField.FieldType), alteredField.FieldType));
                            }
                            else
                            {
                                if (field.GetType().IsValueType)
                                {
                                    field.SetValue(JewelryToEdit, Convert.ChangeType(double.Parse(alteredField.Value), alteredField.FieldType));
                                }
                                else
                                {
                                    field.SetValue(JewelryToEdit, Convert.ChangeType(alteredField.Value, alteredField.FieldType));
                                }
                            }

                            break;
                        }
                    }
                }

                JewelryToEdit.Materials = EditableJewelryMaterials;               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wrong data was provided. Can't add that material. Detailed Error: {ex.Message}");
            }
            finally { IsEditingJewelryAllowed = true; }
        }

        private void ViewInfo(object parameter)
        {
            JewelryDescription = JewelryToEdit.GetDescription();
        }

        public object GetEnumMemberValueByName(string fieldName, Type enumType)
        {
            return Enum.Parse(enumType, fieldName);
        }
    }
}
