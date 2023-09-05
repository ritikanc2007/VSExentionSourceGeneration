using Community.VisualStudio.Toolkit;
using Restarted.Generators.Common.Context;
using Restarted.Generators.FeatureProcessors;
using Restarted.Generators.FeatureProcessors.Controllers;
using Restarted.Generators.FeatureProcessors.CQRS;
using Restarted.Generators.FeatureProcessors.DTO;
using Restarted.Generators.FeatureProcessors.GlobalUsing;
using Restarted.Generators.FeatureProcessors.MapperProfiles;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.FeatureProcessors.Repository;
using Restarted.Generators.Generators.CQRS.Models;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ToolWindow.DynamicForm.Model;
using ToolWindow.Utility;
using WinFormsApp1.DynamicForm.Model;
using static Restarted.Generators.FeatureProcessors.DependencyRegistrationService;
using static Restarted.Generators.FeatureProcessors.GlobalUsing.GlobalUsingService;
using static Restarted.Generators.FeatureProcessors.MapperProfiles.MapperProfileService;

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

            // Map FilePath Convention Information
            MapFilePathConventionAndParams(attributeMetaData, pathSettings.Convention, settings);

            files = DTOService.Process(typeDef, generatorContext, attributeMetaData);

            return files;
        }

        public static List<string> Repository(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {

            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettingsRepo = ConfigurationHelper.PathSettings()[TypeOfCode.Repository];
            var pathSettingsInterface = ConfigurationHelper.PathSettings()[TypeOfCode.RepositoryInterface];
            List<string> files = new List<string>();

            AttributeMetaDataRepo attributeMetaData = settings.ToMetadata<AttributeMetaDataRepo>();
            attributeMetaData.PluralEntityName = CommonHelper.GetPluralName(attributeMetaData.EntityName);
            attributeMetaData.RepositoryName = conventionSetting.Repositories.Replace("{pluralName}", attributeMetaData.PluralEntityName);
            attributeMetaData.InterFaceName = conventionSetting.Interfaces.Replace("{pluralName}", attributeMetaData.PluralEntityName);
            attributeMetaData.GenerationPaths= new Dictionary<string, string>
            {
                { "Repository", pathSettingsRepo.FullPath },
                { "Interface", pathSettingsInterface.FullPath }
            };
            attributeMetaData.NameSpace = new Dictionary<string, string>
            {
                { "Repository", pathSettingsRepo.NameSpace },
                { "Interface", pathSettingsInterface.NameSpace }
            };


            // Map FilePath Convention Information
            // Mapping repository convention
            FileParamInfo repoParams = MapFilePathConventionAndParams(attributeMetaData, pathSettingsRepo.Convention, settings);

            attributeMetaData.PathConventions = new Dictionary<string, FileParamInfo>
            {
                { "Repository", new FileParamInfo(pathSettingsRepo.Convention, repoParams.FeatureName,repoParams.FeatureModuleName,"") },
                { "Interface", new FileParamInfo(pathSettingsInterface.Convention, repoParams.FeatureName,repoParams.FeatureModuleName,"") }
            };
            // Map FilePath Convention Information


            files = RepositoryService.Process(typeDef, generatorContext, attributeMetaData);
            return files;

        }

        public static List<string> CQRS(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {

            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.CQRSActions];
            List<string> files = new List<string>();

            AttributeMetaDataCQRS data = settings.ToMetadata<AttributeMetaDataCQRS>();
            data.GenerationPath = pathSettings.FullPath;
            data.NameSpace=pathSettings.NameSpace;
            data.MethodInfo = new List<CQRSMethodMap>();

            // Map FilePath Convention Information
            MapFilePathConventionAndParams(data, pathSettings.Convention, settings);

            foreach (var method in typeDef.Methods)
            {
                var setting = settings.Where(s => s.QualifiedName == method.QualifiedName).FirstOrDefault();
                string requestType = "Command";
                if (setting !=null && setting.LabelText.ToLower() == "true")
                    requestType = "Query";

                if (setting !=null)
                    data.MethodInfo.Add(new CQRSMethodMap(method.QualifiedName, method.Name, setting.Value, requestType));
            }

            // Generate CQRS
            files = CQRSService.Process(typeDef, generatorContext, data);
            return files;
        }
        public static List<string> ControllersCQRS(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {
            //Class/Interface name conventions
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.Controller];
            List<string> files = new List<string>();


            //START ControllerInf
            AttributeMetaDataController data = settings.ToMetadata<AttributeMetaDataController>();

            data.ControllerName = conventionSetting.Controllers.Replace("{pluralName}", data.PluralName);
            data.GenerationPath = pathSettings.FullPath;
            data.NameSpace = pathSettings.NameSpace;
            data.Actions = new List<ControllerAction>();

            // Map FilePath Convention Information
            MapFilePathConventionAndParams(data, pathSettings.Convention, settings);

            foreach (var method in typeDef.Methods)
            {
                var setting = settings.Where(s => s.QualifiedName == method.QualifiedName).FirstOrDefault();

                if (setting!=null)
                {
                    string cqrSRequestName = setting.Value;
                    //setting.ContollerSetting.

                    data.Actions.Add(new ControllerAction()
                    {
                        CQRSRequestName = cqrSRequestName,
                        HttpAction = setting.ContollerSetting.HTTPAction,
                        QualifiedName= setting.QualifiedName,
                        MethodName = setting.ContollerSetting.MethodName,
                        Route = setting.ContollerSetting.Route,
                        MethodReturnType = method.ReturnType,
                        MethodParameterDefinition=   string.Join(",", method.Arguments.Select(x => ($"{x.Type} {x.Name}"))),
                        MethodParametersString = string.Join(",", method.Arguments.Select(x => x.Name))

                    });
                    files = ControllerService.Process(typeDef, generatorContext, data);
                }
            }
            //END
            return files;
        }

        public static List<string> ControllerWithRepo(GeneratorContext generatorContext, TypeDefinitionInfo typeDef, List<GeneratorSetting> settings)
        {
            //Class/Interface name conventions
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.Controller];
            List<string> files = new List<string>();


            //START ControllerInf
            AttributeMetaDataRepoController data = settings.ToMetadata<AttributeMetaDataRepoController>();


            data.GenerationPath = pathSettings.FullPath;
            data.NameSpace = pathSettings.NameSpace;
            data.EntityName = typeDef.Name;
            data.PluralName = Utility.CommonHelper.GetPluralName(typeDef.Name);
            data.ControllerName = conventionSetting.Controllers.Replace("{pluralName}", data.PluralName);
            // Map FilePath Convention Information
            MapFilePathConventionAndParams(data, pathSettings.Convention, settings);

            files= ControllerRepoService.Process(typeDef, generatorContext, data);

            //END
            return files;
        }

        private static FileParamInfo MapFilePathConventionAndParams(AttributeMetaData data, string pathConvention, List<GeneratorSetting> settings)
        {
            var featureSetting = settings?.Where(o => o.Name =="Feature").FirstOrDefault();
            var featureModuleSetting = settings?.Where(o => o.Name =="FeatureModule").FirstOrDefault();

            string feature = string.Empty;
            string featureModule = string.Empty;
            if (featureSetting!= null && !string.IsNullOrEmpty(featureSetting.Value))
                feature = featureSetting.Value;

            if (featureModuleSetting!= null && !string.IsNullOrEmpty(featureModuleSetting.Value))
                featureModule = featureModuleSetting.Value;


            data.PathConvention = new FileParamInfo(pathConvention, feature, featureModule, "");

            return data.PathConvention;
        }

        internal static List<string> DependencyRegistration(GeneratorContext generatorContext, TypeDefinitionInfo selectedTypeDefinitionInfo, List<GeneratorSetting> settings)
        {
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.DI];
            List<string> files = new List<string>();

            //AttributeMetaDataDI attributeMetaData = settings.ToMetadata<AttributeMetaDataDI>();
            AttributeMetaDataDI attributeMetaData = new AttributeMetaDataDI();

            attributeMetaData.FileName = conventionSetting.DI;

            attributeMetaData.NameSpace = pathSettings.NameSpace;

            // Map FilePath Convention Information
            MapFilePathConventionAndParams(attributeMetaData, pathSettings.Convention, settings);

            files = DependencyRegistrationService.Process(generatorContext, attributeMetaData);

            return files;
        }

        internal static List<string> MapperProfiles(GeneratorContext generatorContext, TypeDefinitionInfo selectedTypeDefinitionInfo, List<GeneratorSetting> settings)
        {
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathSettings = ConfigurationHelper.PathSettings()[TypeOfCode.Mapper];
            List<string> files = new List<string>();

            //AttributeMetaDataDI attributeMetaData = settings.ToMetadata<AttributeMetaDataDI>();
            AttributeMetaDataMapper attributeMetaData = new AttributeMetaDataMapper();

            attributeMetaData.FileName = conventionSetting.Mapper;

            attributeMetaData.NameSpace = pathSettings.NameSpace;

            // Map FilePath Convention Information
            MapFilePathConventionAndParams(attributeMetaData, pathSettings.Convention, settings);

            files = MapperProfileService.Process(generatorContext, attributeMetaData);

            return files;
        }

        internal static List<string> GlobalUsings(GeneratorContext generatorContext, TypeDefinitionInfo selectedTypeDefinitionInfo, List<GeneratorSetting> settings)
        {
            ConventionSetting conventionSetting = ConfigurationHelper.ConventionSettings();
            var pathAppRoot = ConfigurationHelper.PathSettings()[TypeOfCode.ApplicationRootPath];
            var pathCtrlRoot = ConfigurationHelper.PathSettings()[TypeOfCode.ControllersRootPath];
            var pathInfraRoot = ConfigurationHelper.PathSettings()[TypeOfCode.InfrastructureRootPath];
            var pathContraRoot = ConfigurationHelper.PathSettings()[TypeOfCode.ContractsRootPath];
            List<string> files = new List<string>();

            //AttributeMetaDataDI attributeMetaData = settings.ToMetadata<AttributeMetaDataDI>();
            AttributeMetaDataGlobalUsing attributeMetaData = new AttributeMetaDataGlobalUsing();

            attributeMetaData.FileName = conventionSetting.GlobalUsing;

            attributeMetaData.RootProjectPaths = new Dictionary<TypeOfCode, string>
            {
                { TypeOfCode.ControllersRootPath , pathCtrlRoot.FullPath },
                { TypeOfCode.ApplicationRootPath , pathAppRoot.FullPath },
                 {TypeOfCode.InfrastructureRootPath , pathInfraRoot.FullPath },
                 {TypeOfCode.ContractsRootPath , pathContraRoot.FullPath }
            };

            // Map FilePath Convention Information
            // MapFilePathConventionAndParams(attributeMetaData, pathSettings.Convention, settings);

            files = GlobalUsingService.Process(generatorContext, attributeMetaData);

            return files;
        }
    }
}
