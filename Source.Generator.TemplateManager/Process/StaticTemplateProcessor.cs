using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.CQRS.Templates.Commands;
using Restarted.Generators.Generators.CQRS.Templates.Queries;
using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using Source.Generator.TemplateManager.Models.Enums;
using Source.Generator.TemplateManager.Models.FilePathHandlers;
using Source.Generator.TemplateManager.Models.Identifiers;
using Source.Generator.TemplateManager.Templates.Repository.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Source.Generator.TemplateManager.Process
{
    public class StaticTemplateProcessor
    {
        private static string ConstTemplateFileNameNoExt = "";
        public static IProcessorResult Process(TypeOfTemplate codeType, ITemplateParameter parameter, ICodeTemplate codeTemplate = null)
        {
            Dictionary<string, string> replacementParameters = new Dictionary<string, string>();

            switch (codeType)
            {
                case TypeOfTemplate.ControllerWithRepository:
                    ConstTemplateFileNameNoExt = "ControllerWithRepository";
                    replacementParameters = ReplacementHelper.ControllerWithRepository(parameter);

                    break;
                case TypeOfTemplate.ControllerWithCQRS:
                    ConstTemplateFileNameNoExt = "ControllerWithCQRS";
                    replacementParameters = ReplacementHelper.ControllerWithCQRS(parameter);

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

                    break;
                case TypeOfTemplate.Repository:
                    ConstTemplateFileNameNoExt = "ModelsRepository";
                    replacementParameters = ReplacementHelper.Repository(parameter);
                    break;
                case TypeOfTemplate.RepositoryInterface:
                    ConstTemplateFileNameNoExt = "IModelsRepository";
                    replacementParameters = ReplacementHelper.RepositoryInterface(parameter);
                    break;
                case TypeOfTemplate.DTO:
                    ConstTemplateFileNameNoExt = "ModelDTO";
                    replacementParameters = ReplacementHelper.DTO(parameter);
                    break;
                case TypeOfTemplate.DI:
                    ConstTemplateFileNameNoExt = "DependencyRegistrationTemplate";
                    replacementParameters = ReplacementHelper.DependencyRegistration(parameter);
                    break;
                case TypeOfTemplate.Mapper:
                    ConstTemplateFileNameNoExt = "MapperProfileTemplate";
                    replacementParameters = ReplacementHelper.Mappers(parameter);
                    break;
                case TypeOfTemplate.GlobalUsings:
                    ConstTemplateFileNameNoExt = "GlobalUsingTemplate";
                    replacementParameters = ReplacementHelper.GlobalUsings(parameter);
                    break;
                default:
                    break;
            }


            return new ProcessorResult() { SourceCode= Transform(replacementParameters) };
        }

        private static string Transform(Dictionary<string, string> replacementParameters)
        {
            var pathInfo = PathHelper.GetPresetPaths(basePath: "", PathConstants.PresetPathTemplate, ConstTemplateFileNameNoExt);

            var SourceTemplate = File.ReadAllText(pathInfo.Main);

            foreach (var placeHolder in replacementParameters)
            {
                SourceTemplate= SourceTemplate.Replace(placeHolder.Key, placeHolder.Value);
            }

            return SourceTemplate;
        }
    }
}
