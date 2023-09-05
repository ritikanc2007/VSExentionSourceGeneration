using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.CQRS.Templates.Commands;
using Restarted.Generators.Generators.CQRS.Templates.Queries;
using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using Restarted.Generators.FeatureProcessors.Models.Enums;
using Restarted.Generators.FeatureProcessors.Models.FilePathHandlers;
using Restarted.Generators.FeatureProcessors.Models.Identifiers;
using Restarted.Generators.FeatureProcessors.Templates.Repository.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SourceGeneratorParser.Parsers.Helpers;

namespace Restarted.Generators.FeatureProcessors.Process
{
    public class StaticTemplateProcessor
    {
        private static string ConstTemplateFileNameNoExt = "";

        
        public static IProcessorResult Process(TypeOfTemplate codeType, ITemplateParameter parameter, ICodeTemplate codeTemplate = null)
        {
            Dictionary<string, string> replacementParameters = new Dictionary<string, string>();
            PresetType presetType= PresetType.Controller;
            switch (codeType)
            {
                case TypeOfTemplate.ControllerWithRepository:
                    ConstTemplateFileNameNoExt = "ControllerWithRepository";
                    replacementParameters = ReplacementHelper.ControllerWithRepository(parameter);
                    presetType= PresetType.Controller;
                    break;
                case TypeOfTemplate.ControllerWithCQRS:
                    ConstTemplateFileNameNoExt = "ControllerWithCQRS";
                    replacementParameters = ReplacementHelper.ControllerWithCQRS(parameter);


                    presetType= PresetType.Controller;
                    break;
                case TypeOfTemplate.CQRSActions:
                    if (codeTemplate is CQRSModelCommand)
                        ConstTemplateFileNameNoExt = "CQRSModelCommand";
                    else if (codeTemplate is CQRSModelCommandHandler)
                        ConstTemplateFileNameNoExt = "CQRSModelCommandHandler";
                    else if (codeTemplate is CQRSModelCommandValidator)
                        ConstTemplateFileNameNoExt = "CQRSModelCommandValidator";
                    else if (codeTemplate is CQRSModelQueryRequest)
                        ConstTemplateFileNameNoExt = "CQRSModelQueryRequest";
                    else if (codeTemplate is CQRSModelQueryHandler)
                        ConstTemplateFileNameNoExt = "CQRSModelQueryHandler";
                    else if (codeTemplate is CQRSModelQueryValidator)
                        ConstTemplateFileNameNoExt = "CQRSModelQueryValidator";

                    replacementParameters = ReplacementHelper.CQRSActions(parameter);
                    presetType= PresetType.CQRS;
                    break;
                case TypeOfTemplate.Repository:
                    ConstTemplateFileNameNoExt = "ModelsRepository";
                    replacementParameters = ReplacementHelper.Repository(parameter);
                    presetType= PresetType.Repository;
                    break;
                case TypeOfTemplate.RepositoryInterface:
                    ConstTemplateFileNameNoExt = "IModelsRepository";
                    replacementParameters = ReplacementHelper.RepositoryInterface(parameter);
                    presetType= PresetType.Repository;
                    break;
                case TypeOfTemplate.DTO:
                    ConstTemplateFileNameNoExt = "ModelDTO";
                    replacementParameters = ReplacementHelper.DTO(parameter);
                    presetType= PresetType.DTO;
                    break;
                case TypeOfTemplate.DI:
                    ConstTemplateFileNameNoExt = "DependencyRegistrationTemplate";
                    replacementParameters = ReplacementHelper.DependencyRegistration(parameter);
                    presetType= PresetType.DI;
                    break;
                case TypeOfTemplate.Mapper:
                    ConstTemplateFileNameNoExt = "MapperProfileTemplate";
                    replacementParameters = ReplacementHelper.Mappers(parameter);
                    presetType= PresetType.Mappers;
                    break;
                case TypeOfTemplate.GlobalUsings:
                    ConstTemplateFileNameNoExt = "GlobalUsingTemplate";
                    replacementParameters = ReplacementHelper.GlobalUsings(parameter);
                    presetType= PresetType.GlobalUsings;
                    break;
                default:
                    break;
            }


            return new ProcessorResult() { SourceCode= Transform(replacementParameters,presetType) };
        }

        private static string Transform(Dictionary<string, string> replacementParameters,PresetType presetType)
        {

            var pathInfo = PathHelper.GetPresetPaths(basePath: "", PathConstants.PresetPathTemplate, ConstTemplateFileNameNoExt,presetType);

            var SourceTemplate = File.ReadAllText(pathInfo.Main);

            foreach (var placeHolder in replacementParameters)
            {
                SourceTemplate= SourceTemplate.Replace(placeHolder.Key, placeHolder.Value);
            }

            return SourceTemplate;
        }

        private static string TransformAndAddMethod(Dictionary<string, string> replacementParameters, PresetType presetType)
        {
            // Method processor
            // condition if IsMethodGeneration
            // 1. Generate source code for method based on the template
            // 2. Get SourceCode of main file and add method
            // 3. Return source code
            // Can add method to repo and controller but cqrs are new files.. so no change needed for CQRS
            
            if (presetType == PresetType.Controller || presetType == PresetType.Repository)
            {
                // get method templates
                // replace params
                // 

                string sourceOfClass = @"public class Employee{}"; //File.ReadAllText(pathInfo.Main);
                string sourceofMethod = @"public void DoSomething(){}";

                ClassParserHelper.AddNewMethodsToClass(sourceOfClass, new List<string>() {sourceofMethod });
            }
            var pathInfo = PathHelper.GetPresetPaths(basePath: "", PathConstants.PresetPathTemplate, ConstTemplateFileNameNoExt, presetType);

            var SourceTemplate = File.ReadAllText(pathInfo.Main);

            foreach (var placeHolder in replacementParameters)
            {
                SourceTemplate= SourceTemplate.Replace(placeHolder.Key, placeHolder.Value);
            }

            return SourceTemplate;
        }
    }
}
