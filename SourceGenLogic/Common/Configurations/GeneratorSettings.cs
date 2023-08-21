using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Generators.DTO.Attributes;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Generators.Repositories.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static Restarted.Generators.Common.Configurations.GeneratorConfigurations;
using static System.Net.Mime.MediaTypeNames;

namespace Restarted.Generators.Common.Configurations
{


    public static class GeneratorConfigurations
    {


        /// <summary>
        /// All Attributes specific to template will have Feature and FeatureModule parameter
        /// path string will be replaced with application_name,feature,featureModule @"src\{APPLICATION_NAME}.Api.Administration\Controllers\{Feature}\{FeatureModule}"
        /// </summary>
        public static class PathConfiguration
        {

            public static readonly Dictionary<string, string> PathDef = new Dictionary<string, string>()
            {
             {FolderPath.MAPPER_PROFILE_PATH,@"{ApplicationName}.Infrastructure\Persistence\Mapper\"},
             {FolderPath.DEPENDENCY_REGISTRATION_PATH,@"{ApplicationName}.Infrastructure\"},

            {FolderPath.CONTROLLERS_USING_PATH,@"{ApplicationName}.API.Administration\"},
            {FolderPath.APPLICATION_USING_PATH,@"{ApplicationName}.Application\"},
            {FolderPath.INFRASTRUCTURE_USING_PATH,@"{ApplicationName}.Infrastructure\"},

            //{FolderPath.BASE_FOLDER_PATH,@"C:\Users\Narendra\source\repos\SourceGenerator\src\" },
            {FolderPath.BASE_FOLDER_PATH,@"C:\GeneratedCode\" },
            {FolderPath.AlL_PATH_SUFFIX,"Generated"},
            {FolderPath.APPLICATION_NAME,"Restarted.System"},
            {FolderPath.GENERATED_FILE_EXTENSION,".g.cs"},

            {FolderPath.CONTROLLERS_PATH,@"{ApplicationName}.Api.Administration\Controllers\"},
            {FolderPath.REPOSITORY_INTERFACES_PATH,@"{ApplicationName}.Contracts\Interfaces\Persistence\"},
            {FolderPath.REPOSITORY_PATH,@"{ApplicationName}.Infrastructure\Persistence\Repositories\"},

            {FolderPath.DTO_PATH,@"{ApplicationName}.Contracts\DTO\"},
            {FolderPath.COMMON_DTO_PATH,@"{ApplicationName}.Contracts\DTO\Common\"},

            {FolderPath.CQRS_PATH_COMMANDS,@"{ApplicationName}.Application\{FeatureModule}\Commands\{MethodName}\"},
            {FolderPath.CQRS_PATH_QUERIES,@"{ApplicationName}.Application\{FeatureModule}\Queries\{MethodName}\"},
            };

            //public static readonly Dictionary<string, string> PathDef = new Dictionary<string, string>()
            //{
            //{FolderPath.BASE_FOLDER_PATH,@"C:\Users\Narendra\source\repos\Restarted.System\ApiTemplate\src\"},
            //{FolderPath.AlL_PATH_SUFFIX,"Generated"},
            //{FolderPath.APPLICATION_NAME,"Restarted.System"},
            //{FolderPath.GENERATED_FILE_EXTENSION,".g.cs"},

            //{FolderPath.CONTROLLERS_PATH,@"{ApplicationName}.Api.Administration\Controllers\{Feature}\{FeatureModule}\"},
            //{FolderPath.REPOSITORY_INTERFACES_PATH,@"{ApplicationName}.Application\Common\Interfaces\Persistence\{Feature}\{FeatureModule}\"},
            //{FolderPath.REPOSITORY_PATH,@"{ApplicationName}.Infrastructure\Persistence\Repositories\{Feature}\{FeatureModule}\"},

            //{FolderPath.DTO_PATH,@"{ApplicationName}.Application\Common\DTO\{Feature}\{FeatureModule}\"},
            //{FolderPath.COMMON_DTO_PATH,@"{ApplicationName}.Application\Common\DTO\Common\"},

            //{FolderPath.CQRS_PATH_COMMANDS,@"{ApplicationName}.Application\{Feature}\{FeatureModule}\Commands\{MethodName}\"},
            //{FolderPath.CQRS_PATH_QUERIES,@"{ApplicationName}.Application\{Feature}\{FeatureModule}\Queries\{MethodName}\"},
            //};
        }
        public static GeneratorSettings GeneratorSetting()
        {
            GeneratorSettings settings = new GeneratorSettings();

            settings.Templates.Add
               (
               new GeneratorTemplateSetting()
               {
                   Name = "GlobalUsing",
                   FileName = "GlobalUsingTemplate.tt",
                   ParameterSettings = new GlobalUsingParameterSettings()
                   {

                       ControllersPath = FolderPath.CONTROLLERS_USING_PATH,
                       ApplicationPath = FolderPath.APPLICATION_USING_PATH,
                       InfrastructurePath = FolderPath.INFRASTRUCTURE_USING_PATH,
                   }

               });
                settings.Templates.Add
                (
                    new GeneratorTemplateSetting()
                    {
                        Name = "MapperProfile",
                        FileName = "MapperProfileTemplate.tt",
                        GenerationPath = FolderPath.MAPPER_PROFILE_PATH,
  
                    });

                settings.Templates.Add
                     (
                     new GeneratorTemplateSetting()
                     {
                         Name = "DependencyRegistrationProfile",
                         FileName = "DependencyRegistrationTemplate.tt",
                         GenerationPath = FolderPath.DEPENDENCY_REGISTRATION_PATH,

                     });
                settings.Templates.Add
                    (
                    new GeneratorTemplateSetting()
                    {
                        Name = "ModelDTO",
                        FileName = "ModelDTO.tt",
                        AttributeFileName = "GeneratorDtoAttribute",
                        AttributeName = "GeneratorDto",
                        GenerationPath = FolderPath.DTO_PATH,
                        ParameterSettings = new ModelDTOParameterSetting()
                        {

                            Convention = "{entity}DTO",
                            AllPropertiesNullable = false
                        }

                    });

            settings.Templates.Add
               (
               new GeneratorTemplateSetting()
               {
                   Name = "ModelsRepository",
                   FileName = "ModelsRepository.tt",
                   AttributeFileName = "GeneratorRepositoryAttribute",
                   AttributeName = "GeneratorRepository",
                   GenerationPath = FolderPath.REPOSITORY_PATH,

                   ParameterSettings = new ModelsRepositoryParameterSetting()
                   {
                       Convention = "{entity}sRepository"
                   }

               });

            settings.Templates.Add
           (
           new GeneratorTemplateSetting()
           {
               Name = "IModelsRepository",
               FileName = "IModelsRepository.tt",
               AttributeFileName = "GeneratorRepositoryInterfaceAttribute",
               AttributeName = "GeneratorRepositoryInterface",
               GenerationPath = FolderPath.REPOSITORY_INTERFACES_PATH,
               ParameterSettings = new ModelsRepositoryParameterSetting()
               {
                   Convention = "I{entity}sRepository"
               }

           });
            settings.Templates.Add
                  (
                  new GeneratorTemplateSetting()
                  {
                      Name = "CQRSModelQuery",
                      FileName = "CQRSModelQuery.tt",
                      AttributeFileName = "GeneratorCQRSAttribute",
                      AttributeName = "GeneratorCQRS",
                      GenerationPath = FolderPath.CQRS_PATH_QUERIES,
                      ParameterSettings = new CQRSTemplateParameterSettings()
                      {
                          Convention = "{MethodName}"
                      }
                  });

            settings.Templates.Add
                 (
                 new GeneratorTemplateSetting()
                 {
                     Name = "CQRSModelCommand",
                     FileName = "CQRSModelCommand.tt",
                     AttributeFileName = "GeneratorCQRSAttribute",
                     AttributeName = "GeneratorCQRS",
                     GenerationPath = FolderPath.CQRS_PATH_COMMANDS,
                     ParameterSettings = new CQRSTemplateParameterSettings()
                     {
                         Convention = "{MethodName}Command"
                     }
                 });

            settings.Templates.Add
               (
               new GeneratorTemplateSetting()
               {
                   Name = "ModelsController",
                   FileName = "ModelsController.tt",
                   AttributeFileName = "GeneratorApiAttribute",
                   AttributeName = "GeneratorApi",
                   GenerationPath = FolderPath.CONTROLLERS_PATH,
                   ParameterSettings = new ApiTemplateParameterSettings()
                   {
                       Convention = "{Model}sController"
                   }
               });
            return settings;
        }


    }
    public class GlobalUsingParameterSettings : TemplateParameterSettingBase
    {
        public string ControllersPath { get; set; }
        public string ApplicationPath { get; set; }
        public string InfrastructurePath { get; set; }
    }

    public class ApiTemplateParameterSettings : TemplateParameterSettingBase
    {
    }
    public class CQRSTemplateParameterSettings : TemplateParameterSettingBase
    {
    }
    public class ModelsRepositoryParameterSetting : TemplateParameterSettingBase
    {

    }
    public class GeneratorSettings
    {


        public List<GeneratorTemplateSetting> Templates { get; set; } = new List<GeneratorTemplateSetting>();

    }

    public static class GeneratorSettingsExtension
    {
        public static GeneratorTemplateSetting GetSetting(this GeneratorSettings settings, string templateName)
        {
            return settings.Templates.Where(o => o.Name.Equals(templateName)).FirstOrDefault();
        }
    }

    public class GeneratorTemplateSetting
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string AttributeFileName { get; set; }

        public string AttributeName { get; set; }

        public string GenerationPath { get; set; }
        public FileLocationInfo FilePathInformation { get; set; }

        public TemplateParameterSettingBase ParameterSettings { get; set; }


    }

    public class TemplateParameterSettingBase
    {
        public string Convention { get; set; }
    }
    public class ModelDTOParameterSetting : TemplateParameterSettingBase
    {

        public string Convention { get; set; } = "";
        public bool AllPropertiesNullable { get; set; }

    }

    public class FileLocationInfo
    {
        public string BasePath { get; set; }
        public string GeneratedFileExtension { get; set; } = ".g.cs";
        public string GeneratedFileDirectory { get; set; }
        public string PartialFileDirectory { get; set; }


    }
}
