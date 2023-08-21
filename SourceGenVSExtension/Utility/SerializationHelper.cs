using Newtonsoft.Json;
using Restarted.Generators.Common.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolWindow.DynamicForm.Forms;
using WinFormsApp1.DynamicForm.Model;

namespace ToolWindow.Utility
{
    internal static class SerializationHelper
    {
        public static void SaveSettings(List<GeneratorSetting> settings, string fileName)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            File.WriteAllText(fileName, json);
        }

        public static List<GeneratorSetting> DeserializeSettings(string fileName)
        {
            List<GeneratorSetting> settings = null;


            if (File.Exists(fileName))
            {
                string jsonSaved = File.ReadAllText(fileName);
                settings = JsonConvert.DeserializeObject<List<GeneratorSetting>>(jsonSaved);

            }

            return settings;
        }

        public static void SavePathSettings(Dictionary<TypeOfCode,PathNameSpaceInfo> settings, string fileName)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            File.WriteAllText(fileName, json);
        }

        public static Dictionary<TypeOfCode, PathNameSpaceInfo>  DeserializePathSettings(string fileName)
        {
            Dictionary<TypeOfCode, PathNameSpaceInfo>  settings = null;


            if (File.Exists(fileName))
            {
                string jsonSaved = File.ReadAllText(fileName);
                settings = JsonConvert.DeserializeObject<Dictionary<TypeOfCode,PathNameSpaceInfo>>(jsonSaved);

            }

            return settings;
        }
    }
}
