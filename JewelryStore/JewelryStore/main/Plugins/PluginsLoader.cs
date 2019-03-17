using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Plugins {
    public static class PluginsLoader<T> {

        /// <summary>
        /// Loads all available plugins matching type T
        /// </summary>
        /// <returns></returns>
        public static List<T> Load()
        {
            string AddInDir =
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var AddInAssemblies = Directory.EnumerateFiles(AddInDir, "*.dll");
             
            var Plugins =
            from file in AddInAssemblies
            let assembly = Assembly.LoadFrom(file)
            from t in assembly.ExportedTypes 
                                             
            where t.IsClass &&
            typeof(T).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo())
            select t;

            var result = new List<T>();

            foreach (Type t in Plugins)
            {
                result.Add((T)Activator.CreateInstance(t));
            }

            return result;
        }
    }
}
