using Community.VisualStudio.Toolkit;
using Restarted.Generators.Common.Context;
using Restarted.Generators.FeatureProcessors.Controllers;
using Restarted.Generators.FeatureProcessors.CQRS;
using Restarted.Generators.FeatureProcessors.DTO;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.FeatureProcessors.Repository;
using Restarted.Generators.Generators.CQRS.Models;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using ToolWindow.DynamicForm.Model;
using ToolWindow.Utility;
using WinFormsApp1.DynamicForm.Model;

namespace ToolWindow.ProcessRequest
{
    internal class ProcessGeneration
    {

        public static List<string> DTO(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.DTO];
            List<string> files = new List<string>();

            AttributeMetaDataDto attributeMetaData = settings.ToMetadata<AttributeMetaDataDto>();

            if (string.IsNullOrEmpty(attributeMetaData.GenerationPath) &&  attributeMetaData.GenerationPath != pathSettings.FullPath)
                attributeMetaData.GenerationPath = pathSettings.FullPath;

            attributeMetaData.NameSpace = pathSettings.NameSpace;

            files = DTOService.Process(typeDef, generatorContext, attributeMetaData);

            return files;
        }

        public static List<string> Repository(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {

            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettingsRepo = ConfigurationHelper.PathSettings()[TypeOfCode.Repository];
            var pathSettingsInterface = ConfigurationHelper.PathSettings()[TypeOfCode.Repository];
            List<string> files = new List<string>();

            AttributeMetaDataRepo attributeMetaData = settings.ToMetadata<AttributeMetaDataRepo>();
            attributeMetaData.PluralEntityName = CommonHelper.GetPluralName(attributeMetaData.EntityName);
            attributeMetaData.RepositoryName = conventionSetting.Repositories.Replace("{pluralName}", attributeMetaData.PluralEntityName);
            attributeMetaData.InterFaceName = conventionSetting.Interfaces.Replace("{pluralName}", attributeMetaData.PluralEntityName);

            attributeMetaData.GenerationPaths.Add("Repository", pathSettingsRepo.FullPath);
            attributeMetaData.GenerationPaths.Add("Interface", pathSettingsInterface.FullPath);
            attributeMetaData.NameSpace.Add("Repository", pathSettingsRepo.NameSpace);
            attributeMetaData.NameSpace.Add("Interface", pathSettingsInterface.NameSpace);
            files = RepositoryService.Process(typeDef, generatorContext, attributeMetaData);
            return files;

        }

        public static List<string> CQRS(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {

            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.CQRSActions];
            List<string> files = new List<string>();

            AttributeMetaDataCQRS attributeMetaData = settings.ToMetadata<AttributeMetaDataCQRS>();
            attributeMetaData.GenerationPath = pathSettings.FullPath;
            attributeMetaData.NameSpace=pathSettings.NameSpace;
            attributeMetaData.MethodInfo = new List<CQRSMethodMap>();

            foreach (var method in typeDef.Methods)
            {
                var setting = settings.Where(s => s.Name == method.Name).FirstOrDefault();
                string requestType = "Command";
                if (setting.LabelText.ToLower() == "true")
                    requestType = "Query";

                attributeMetaData.MethodInfo.Add(new CQRSMethodMap(method.Name, setting.Value, requestType));
            }


            // Generate CQRS
            files = CQRSService.Process(typeDef, generatorContext, attributeMetaData);
            return files;
        }
        public static List<string> ControllersCQRS(GeneratorContext generatorContext, TypeDefinitionInfo typeDef,List<GeneratorSetting> settings)
        {
           
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.Controller];
            List<string> files = new List<string>();


            //START ControllerInf
            AttributeMetaDataController data = settings.ToMetadata<AttributeMetaDataController>();
            data.ControllerName = conventionSetting.Controllers.Replace("{pluralName}", data.PluralName);
            data.GenerationPath = pathSettings.FullPath;
            data.NameSpace = pathSettings.NameSpace;
            data.Actions = new List<ControllerAction>();

            foreach (var method in typeDef.Methods)
            {
                var setting = settings.Where(s => s.Name == method.Name).FirstOrDefault();
                string cqrSRequestName = setting.Value;
                //setting.ContollerSetting.

                data.Actions.Add(new ControllerAction()
                {
                    CQRSRequestName = cqrSRequestName,
                    HttpAction = setting.ContollerSetting.HTTPAction,
                    MethodName = setting.ContollerSetting.MethodName,
                    Route = setting.ContollerSetting.Route,
                    MethodReturnType = method.ReturnType,
                    MethodParameterDefinition=   string.Join(",", method.Arguments.Select(x => ($"{x.Type} {x.Name}"))),
                    MethodParametersString = string.Join(",", method.Arguments.Select(x => x.Name))

                });
                files = ControllerService.Process(typeDef, generatorContext, data);
            }
            //END
            return files;
        }
    }
}
