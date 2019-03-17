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
using JewelryStore.main.Extensions;

namespace JewelryStore.main.Serialization {
    public class TextSerializer : IJewelrySerializer {

        /// <summary>
        /// Used to deserialize a text stream contained in a file.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public object Deserialize(Type t, FileStream source)
        {
            string serializedString;

            using (var reader = new StreamReader(source))
            {
                serializedString = reader.ReadToEnd();
            }

            serializedString = serializedString.Replace("\n", "");

            return ParseObject(serializedString);
        }


        /// <summary>
        /// Used to serialize an USER-DEFINED OBJECT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destination"></param>

        public string Serialize(object value)
        {
            return Serialize(value, false);           
        }

        private object ParseObject(string serializedObject)
        {
            var baseClass = serializedObject.GetValueInBetween("'", "{", false);

            var Class = Assembly.GetExecutingAssembly().CreateInstance(baseClass);

            //remove what we've already parsed

            serializedObject = serializedObject.Substring(serializedObject.IndexOf("{") + 1);

            //TODO: Create list of objects, arrays and properties inside this object, fill them. 
            
            var classProperties = Class.GetType().GetProperties();

            foreach(var property in classProperties)
            {
                var propertyName = serializedObject.GetValueInBetween("|", "=", false);
                serializedObject = serializedObject.Substring(serializedObject.IndexOf("==") + 2);

                //find property matching parsed name

                foreach (var nestedProperty in classProperties)
                {
                    if(propertyName == nestedProperty.Name)
                    {
                        var attribs = property.GetCustomAttributes();

                        if(attribs.Any(x => x.GetType() == typeof(TextFieldAttribute)))
                        {
                            var value = serializedObject.Substring(0, serializedObject.IndexOf(";"));

                            FillTheProperty(propertyName, value, ref Class);

                            serializedObject = serializedObject.Substring(serializedObject.IndexOf(";") + 1);

                            break;
                        }

                        if (attribs.Any(x => x.GetType() == typeof(TextArrayAttribute)))
                        {

                            var value = ParseList(serializedObject.GetValueInBetween("[", "]", true));

                            serializedObject = serializedObject.Remove(serializedObject.IndexOf("["), 1);

                            serializedObject = serializedObject.Remove(serializedObject.LastIndexOf("]"), 1);

                            FillTheProperty(propertyName, value, ref Class);
                            break;
                        }

                        if (attribs.Any(x => x.GetType() == typeof(TextClassAttribute)))
                        {
                            var value = ParseObject(serializedObject);

                            FillTheProperty(propertyName, value, ref Class);
                            break;
                        }
                    }
                }
            }

            return Class;

        }

        /// <summary>
        /// Only supports parsing list with complex objects inside for now.
        /// </summary>
        /// <param name="serializedArray"></param>
        /// <returns></returns>
        private List<object> ParseList(string serializedArray)
        {
            var members = new List<string>();

            var builder = new StringBuilder();

            int amountBracesOpened = 0;

            foreach(var ch in serializedArray)
            {
                if (ch == '[')
                    ++amountBracesOpened;

                if (ch == ']')
                    --amountBracesOpened;

                if (ch == ',' && amountBracesOpened.Equals(0))
                {
                    members.Add(builder.ToString());
                    builder.Clear();
                    continue;
                }

                builder.Append(ch);
            }

            //last member is not followed by comma, so add it now.

            if (amountBracesOpened.Equals(0) && builder.Length != 0)
            {
                members.Add(builder.ToString());
            }

            var result = new List<object>();

            foreach(var member in members)
            {
                result.Add(ParseObject(member));
            }

            return result;
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
                        value = ParseEnumVariable((string)value, property.PropertyType);
                    }

                    if (property.PropertyType.IsValueType && !property.PropertyType.IsEnum)
                    {
                        value = double.Parse((string)value);
                    }

                    if (property.PropertyType.IsGenericList())
                    {
                        value = ((List<object>)value).ConvertList(property.PropertyType);
                    }

                    property.SetValue(obj, value);                  
                }
            }
        }

        private object ParseEnumVariable(string memberName, Type enumType)
        {
            return Enum.Parse(enumType, memberName);
        }

        private string Serialize(object member, bool x)
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
