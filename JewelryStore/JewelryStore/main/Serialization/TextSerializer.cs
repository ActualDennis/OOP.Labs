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
    public class TextSerializer : IJewelrySerializer {
        public List<Jewelry> Deserialize(FileStream source)
        {
            //Activator.CreateInstanceFrom(typeof(Jewelry).Assembly.Location, )
            throw new NotImplementedException();
        }
        public void Serialize(object value, FileStream destination)
        {
            string result = Serialize(value);

            using (var writer = new StreamWriter(destination))
            {
                writer.Write(result);
            }
        }

        private string Serialize(object member)
        {
            var result = string.Empty;

            if (IsSerializableClass(member))
            {
                result += $"\n'{member.GetType().Name}'";
                result += "\n{";

                var fields = GetMembersWithTextFieldAttribute(member);

                foreach (var field in fields)
                {
                    result += $"\n''{field.Name}''=={field.GetMethod.Invoke(member, null)};";
                }

                var arrays = GetMembersWithTextArrayAttribute(member);

                foreach (var array in arrays)
                {
                    var ienumerable = (IEnumerable<object>)array.GetMethod.Invoke(member, null);
                    result += $"''{array.Name}''==";
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
