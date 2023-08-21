using Restarted.Generators.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ToolWindow.DynamicForm.Forms;
using ToolWindow.Utility;
using WinFormsApp1.DynamicForm.Model;

namespace ToolWindow.DynamicForm.Model
{
    public static class ConfigurationHelper
    {
        public static Dictionary<TypeOfCode, PathNameSpaceInfo> PathSettings()
        {
            //TODO: Remove this and change method to deserializePATHsettings
            Dictionary<TypeOfCode, PathNameSpaceInfo> settings =  SerializationHelper.DeserializePathSettings(GeneratorConstants.CONST_GENERATOR_PATHS_FILE);
            if (settings == null) return null;
            return settings; //settings.ToSettings<PathSetting>();
        }

        public static ConventionSetting ConventionSettings()
        {
            List<GeneratorSetting> settings = SerializationHelper.DeserializeSettings(GeneratorConstants.CONST_GENERATOR_CONVENTION_FILE);
            if (settings == null) return null;
            return settings.ToSettings<ConventionSetting>();
        }
    }

    public class PathSetting
    {
        public string BasePath { get; set; }
        public string NamespaceRoot { get; set; }
        public string DTOS { get; set; }

        public string Controllers { get; set; }

        public string Repositories { get; set; }
        public string Interfaces { get; set; }

        public string CQRS { get; set; }

        public string DI { get; set; }

        public string Mapper { get; set; }

    }

    public class ConventionSetting
    {
        public string DTOS { get; set; }

        public string Controllers { get; set; }

        public string Repositories { get; set; }
        public string Interfaces { get; set; }

        public string CQRS { get; set; }

       

    }
}
