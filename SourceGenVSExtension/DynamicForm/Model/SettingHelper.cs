using Restarted.Generators.Common.Context;
using SourceGeneratorParser.Models.Metadata;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToolWindow.DynamicForm.Model;
using ToolWindow.Utility;

namespace WinFormsApp1.DynamicForm.Model
{
    internal static class SettingHelper
    {

        public const string ConstAllPropertiesNull = "AllPropertiesNullable";
        public const string ConstMembers = "Members";
        public const string ConstGeneratedFilePath = "GenerationPath";
        public const string ConstConvension = "Convention";
        public const string ConstName = "Name";
        private const string ConstDTOName = "DTOName";
        private const string ConstDbContextName = "DatabaseContextName";
        private const string ConstPluralName = "PluralEntityName";
        private const string ConstEntityName = "EntityName";
        private const string ConstIncludes = "Includes";

        public static List<GeneratorSetting> GetGenerateAllSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            string Convension = "{entity}DTO";
            string nameValue = Convension.Replace("{entity}", typeDefinitionInfo.Name);
            string AllPropertiesNull = "true";
            string pluralName = CommonHelper.GetPluralName(typeDefinitionInfo.Name);
            string columns = ToMemberString(typeDefinitionInfo.Members);


            //Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));
          

            // settings.Add(new GeneratorSetting("Convention", "Convention",Convension, ControlType.text)); // Remove
            settings.Add(new GeneratorSetting("Name", "DTO Name", nameValue, ControlType.text));
            //settings.Add(new GeneratorSetting("PluralName", "Plural", pluralName, ControlType.text)); // Remove 
            settings.Add(new GeneratorSetting("AllPropertiesNull", "All Null Properties?", AllPropertiesNull, ControlType.CheckBox));
            settings.Add(new GeneratorSetting("Columns", "Columns", columns, ControlType.textArea));
            settings.Add(new GeneratorSetting("GeneratedFilePath", "Generation Path", currentFolderPath, ControlType.FileDialog));
            return settings;

        }


        public static List<GeneratorSetting> GetGenerateDTOSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            var conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.DTO];

            string Convension = conventionSetting.DTOS;
            string nameValue = Convension.Replace("{entity}", typeDefinitionInfo.Name);
            string AllPropertiesNull = "true";
           
            string members = ToMemberString(typeDefinitionInfo.Members);

            //START Add Feature and FeatureModule
            //Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));

            //END FEATURE
            settings.Add(new GeneratorSetting(ConstName, "DTO Name", nameValue, ControlType.text));
            settings.Add(new GeneratorSetting(ConstAllPropertiesNull, "All Null Properties?", AllPropertiesNull, ControlType.CheckBox));
            settings.Add(new GeneratorSetting(ConstMembers, "Members", members, ControlType.textArea));
            settings.Add(new GeneratorSetting(ConstGeneratedFilePath, "Generation Path", pathSettings.FullPath, ControlType.FileDialog));
            return settings;

        }
        public static List<GeneratorSetting> GetGenerateRepositorySettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            var dtoConvention = ConfigurationHelper.ConventionSettings().DTOS;
            //START Add Feature and FeatureModule
            //Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));

            //END FEATURE
            settings.Add(new GeneratorSetting(ConstDTOName, "DTO Name", dtoConvention.Replace("{entity}", typeDefinitionInfo.Name), ControlType.text));
            settings.Add(new GeneratorSetting(ConstEntityName, "Entity", typeDefinitionInfo.Name, ControlType.text));
           // settings.Add(new GeneratorSetting(ConstIncludes, "Includes (,)", typeDefinitionInfo.Name, ControlType.text));
            settings.Add(new GeneratorSetting(ConstDbContextName, "DB Context", "dbContext", ControlType.text)); // Remove 
            return settings;

        }
        public static List<GeneratorSetting> GetGenerateControllerWithRepoSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            var dtoConvention = ConfigurationHelper.ConventionSettings().Controllers;
            //START Add Feature and FeatureModule
            //Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));

            //END FEATURE
            return settings;

        }
        public static List<GeneratorSetting> GetGenerateCQRSSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            //START Add Feature and FeatureModule
            //Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));


            //END FEATURE
            var eventControl = new GeneratorSetting("EntityName", "Entity", "", ControlType.text, isEventControl: true, eventMethod: "EntityPluralNameAction");
            var dependentControl = new GeneratorSetting("PluralName", "Plural", "", ControlType.text);
            settings.Add(eventControl);
            settings.Add(dependentControl); // Remove 
            

            List<string> Methods = new List<string>();
            foreach (var method in typeDefinitionInfo?.Methods)
            {
                string methodName = method.Name;
                string qualifiedName = method.QualifiedName;
                
                settings.Add(new GeneratorSetting(methodName, methodName, methodName, ControlType.CQRSControl,qualifiedName: qualifiedName));
            }

           

            return settings;

        }
        internal static List<GeneratorSetting> GetGenerateCTRLSettings(TypeDefinitionInfo typeDefinitionInfo, string initialFolderDirectory)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            //START Add Feature and FeatureModule
            settings.Add(new GeneratorSetting("Feature", "Feature", "", ControlType.text));
            settings.Add(new GeneratorSetting("FeatureModule", "Module", "", ControlType.text));
            //END FEATURE

            var eventControl = new GeneratorSetting("EntityName", "Entity", "", ControlType.text, isEventControl: true, eventMethod: "EntityPluralNameAction");
            var dependentControl = new GeneratorSetting("PluralName", "Plural", "", ControlType.text);
            settings.Add(eventControl);
            settings.Add(dependentControl); // Remove 


            List<string> Methods = new List<string>();
            foreach (var method in typeDefinitionInfo?.Methods)
            {
                string methodName = method.Name;
                settings.Add(new GeneratorSetting(methodName, methodName, methodName, ControlType.CQRSControl));
            }

            return settings;
        }

        public static List<GeneratorSetting> GetPathSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();

            var basePathSetting = new GeneratorSetting("BasePath", "Base ", "", ControlType.FileDialog, isEventControl: true, eventMethod: "BasePathDependentAction");
            settings.Add(new GeneratorSetting("NamespaceRoot", "Namespace Root", "Inventory.System", ControlType.text)); // Remove

            settings.Add(basePathSetting);
           
            settings.Add(new GeneratorSetting("DTOS", "DTO ", "", ControlType.FileDialog)); // Remove
            settings.Add(new GeneratorSetting("Controllers", "Controller", "", ControlType.FileDialog));
            settings.Add(new GeneratorSetting("Repositories", "Repository", "", ControlType.FileDialog)); // Remove 
            settings.Add(new GeneratorSetting("Interfaces", "(I)Repository", "", ControlType.FileDialog)); // Remove 
            settings.Add(new GeneratorSetting("CQRS", "CQRS", "", ControlType.FileDialog));
            settings.Add(new GeneratorSetting("DI", "DI Registration", "", ControlType.FileDialog));
            settings.Add(new GeneratorSetting("Mapper", "Mapper profile", "", ControlType.FileDialog));
            return settings;

        }

        public static List<GeneratorSetting> GetConventionSettings(TypeDefinitionInfo typeDefinitionInfo, string currentFolderPath)
        {
            List<GeneratorSetting> settings = new List<GeneratorSetting>();


            settings.Add(new GeneratorSetting("DTOS", "DTO ", "{entity}DTO", ControlType.text)); // Remove
            settings.Add(new GeneratorSetting("Controllers", "Controller", "{pluralName}Controller", ControlType.text));
            settings.Add(new GeneratorSetting("Repositories", "Repository", "{pluralName}Repository", ControlType.text)); // Remove 
            settings.Add(new GeneratorSetting("Interfaces", "(I)Repository", "I{pluralName}Repository", ControlType.text)); // Remove 
            settings.Add(new GeneratorSetting("CQRS", "CQRS", "{methodName}{entity}Query", ControlType.text));
            settings.Add(new GeneratorSetting("DI", "DI File Name", "DependencyRegistration", ControlType.text));
            settings.Add(new GeneratorSetting("Mapper", "Mapper Profile", "MapperProfile", ControlType.text));
            settings.Add(new GeneratorSetting("GlobalUsing", "GlobalUsing", "GlobalUsing", ControlType.text));
            return settings;

        }

     
        public static string ToMemberString(List<MemberItemInfo> members)
        {
            StringBuilder bldr = new StringBuilder();

            members.ForEach(o => bldr.Append(o.Name + ","));
            string memberString = bldr.ToString();
            memberString = memberString.Substring(0, memberString.Length - 1);
            return memberString;
        }
       

    }
}
