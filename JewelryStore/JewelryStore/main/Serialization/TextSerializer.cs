using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using JewelryOop;
using JewelryStore.main.Attributes;

namespace JewelryStore.main.Serialization {
    public class TextSerializer {

        public object Deserialize(FileStream source)
        {
            string serializedString;

            using (var reader = new StreamReader(source))
            {
                serializedString = reader.ReadToEnd();
            }

            serializedString = serializedString.Replace("\n", "");

            return ParseObject(serializedString);
        }
        public void Serialize(object value, FileStream destination)
        {
            string result = Serialize(value);

            using (var writer = new StreamWriter(destination))
            {
                writer.Write(result);
            }
           
        }

        private object ParseObject(string serializedObject)
        {
            var baseClass = serializedObject.Substring(serializedObject.IndexOf("'") + 1,
                serializedObject.IndexOf("{") - serializedObject.IndexOf("'") - 1 - 1);

            var Class = Assembly.GetExecutingAssembly().CreateInstance(baseClass);

            //TODO: Create list of objects, arrays and properties inside this object, fill them. 

        }

        private List<object> ParseList(string serializedArray)
        {
            //TODO: Create list of objects inside.
        }

        private void FillTheProperty(string name, object value, ref object obj)
        {
            var props = obj.GetType().GetProperties();

            foreach(var property in props)
            {
                if(property.Name == name)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        value = ParseEnumVariable(property.Name, property.PropertyType);
                    }

                    if (property.PropertyType.IsValueType)
                    {
                        value = double.Parse(name);
                    }

                    property.SetValue(obj, value);                  
                }
            }
        }

        private object ParseEnumVariable(string memberName, Type enumType)
        {
            return Enum.Parse(enumType, memberName);
        }

        private string Serialize(object member)
        {
            var result = string.Empty;

            if (IsSerializableClass(member))
            {
                result += $"\n'{member.GetType().FullName}'";
                result += "\n{";

                var fields = GetMembersWithTextFieldAttribute(member);

                foreach (var field in fields)
                {
                    result += $"\n|{field.Name}|=={field.GetMethod.Invoke(member, null)};";
                }

                var arrays = GetMembersWithTextArrayAttribute(member);

                foreach (var array in arrays)
                {
                    var ienumerable = (IEnumerable<object>)array.GetMethod.Invoke(member, null);
                    result += $"|{array.Name}|==";
                    result += "\n[";

                    foreach (var item in ienumerable)
                    {
                        result += $"{Serialize(item)}\n";
                        result += ",";
                    }

                    result = result.Remove(result.Length - 1);

                    result += "\n]";
                }

                result += "\n}";
            }
            else
            {
                result = $"{member}";
            }


            return result;
        }

        private List<PropertyInfo> GetMembersWithTextFieldAttribute(object member)
        {
            var members = member.GetType().GetProperties();
            var result = new List<PropertyInfo>();
            foreach (var mmbr in members)
            {
                var attributes = mmbr.GetCustomAttributes(false);

                if (attributes.FirstOrDefault((x) => x.GetType() == typeof(TextFieldAttribute))!= null)
                {
                    result.Add(mmbr);
                }
            }

            return result;
        }

        private List<PropertyInfo> GetMembersWithTextArrayAttribute(object member)
        {
            var members = member.GetType().GetProperties();
            var result = new List<PropertyInfo>();
            foreach (var mmbr in members)
            {
                var attributes = mmbr.GetCustomAttributes(false);

                if (attributes.FirstOrDefault((x) => x.GetType() == typeof(TextArrayAttribute)) != null)
                {
                    result.Add(mmbr);
                }
            }

            return result;
        }

        private bool IsSerializableClass(object obj)
        {
            return obj.GetType().GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(TextClassAttribute)) != null;
        }

    }
}
