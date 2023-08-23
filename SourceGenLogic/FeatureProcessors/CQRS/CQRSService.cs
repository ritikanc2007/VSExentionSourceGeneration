using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Definitions.Extensions;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Generators.CQRS.Templates.Commands;
using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.CQRS.Templates.Queries;
using Restarted.Generators.Processor.Models;
using System.IO;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Models.Metadata;

namespace Restarted.Generators.FeatureProcessors.CQRS
{
    public static class CQRSService
    {
        private const string ConstGeneratorAttribute = "GeneratorCQRS";
        private const string ConstFeatureName = "featureName";
        private const string ConstFeatureModuleName = "featureModuleName";
        private const string ConstGenerationPath = "generationPath";
        private const string ConstConvention = "convention";
        private const string ConstRequestType = "RequestType";


        public static List<string> Process(TypeDefinitionInfo typeDefinitionInfo, GeneratorContext generatorContext, AttributeMetaDataCQRS data)
        {

            List<string> files = new List<string>();

          
            foreach (var methodDef in typeDefinitionInfo.Methods)
            {

                var methodData = data.MethodInfo.Where(o => o.MethodName == methodDef.Name).FirstOrDefault();

                var finalReplacedPath = FileService.ConventionBasedPath(data.NameSpace, data.PathConvention.ConventionPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, methodDef.Name);
                if (methodData.RequestType == "Command")
                {
                    finalReplacedPath.FolderPath = finalReplacedPath.FolderPath.Replace("{RequestType}", "Commands");
                    finalReplacedPath.NameSpacePath = finalReplacedPath.NameSpacePath.Replace("{RequestType}", "Commands");
                }
                else
                {
                    finalReplacedPath.FolderPath = finalReplacedPath.FolderPath.Replace("{RequestType}", "Queries");
                    finalReplacedPath.NameSpacePath = finalReplacedPath.NameSpacePath.Replace("{RequestType}", "Queries");
                }
                // Transform Template
                var result = ProcessCQRSTemplates(typeDefinitionInfo,generatorContext, finalReplacedPath.NameSpacePath, finalReplacedPath.FolderPath, methodData.RequestType,  methodDef, methodData.CQRSRequestName, data.PluralName);

                //GENERATORCONTEXT Adding namesapces -- MOVED INSIDE TO CAPTURE ALL FILES
               // generatorContext.NameSpaces.Add(new NameSpaceInfo(TypeOfCode.CQRSActions, methodData.CQRSRequestName, NameSpacePath, data.GenerationPath));

            }


            return files;


        }



        private static List<string> ProcessCQRSTemplates(TypeDefinitionInfo typeDefinitionInfo,GeneratorContext generatorContext,string nameSpacePath, string generationPath, string requestType,  MethodItemInfo methodDefinitionInfo, string cqrsRequestName,string pluralName)
        {
            

            

            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ITemplateParameter parameter = new CQRSTemplateParameter(nameSpacePath, cqrsRequestName, typeDefinitionInfo, methodDefinitionInfo, cqrsRequestName, pluralName);
            List<string> files = new List<string>();

            List<IProcessorResult> results = new List<IProcessorResult>();
            Dictionary<string, ICodeTemplate> codeTemplates = new Dictionary<string, ICodeTemplate>();

            if (requestType.Equals("Query"))
            {
                //add queries
                codeTemplates.Add(cqrsRequestName, new CQRSModelQueryRequest(parameter));
                codeTemplates.Add(cqrsRequestName+"Handler", new CQRSModelQueryHandler(parameter));
                codeTemplates.Add(cqrsRequestName+"Validator", new CQRSModelQueryValidator(parameter));
            }
            else
            {
                // Add commands
                codeTemplates.Add(cqrsRequestName, new CQRSModelCommand(parameter));
                codeTemplates.Add(cqrsRequestName+"Handler", new CQRSModelCommandHandler(parameter));
                codeTemplates.Add(cqrsRequestName+"Validator", new CQRSModelCommandValidator(parameter));
            }

            foreach (string requestName in codeTemplates.Keys)
            {
                parameter.SourceFileName = requestName;

                var codeTemplate = codeTemplates[requestName];
                var result = processor.Process(codeTemplate);
                string pathGenerated = FileService.GenerateSourceAtFolderLocation(generationPath, requestName, result.SourceCode);
                files.Add(pathGenerated);
                results.Add(result);

                //GENERATORCONTEXT Adding namesapces
                generatorContext.NameSpaces.Add(new NameSpaceInfo(TypeOfCode.CQRSActions, parameter.SourceFileName, nameSpacePath, pathGenerated));

            }


            return files;

        }



      
    }
    public class AttributeMetaDataCQRS : AttributeMetaData
    {


        public string EntityName { get; set; }
        public string PluralName { get; set; }

        public List<CQRSMethodMap> MethodInfo { get; set; }

        public string NameSpace { get; set; }
    }

    public class CQRSMethodMap
    {
        public CQRSMethodMap(string methodName, string cQRSRequestName, string requestType)
        {
            MethodName=methodName;
            CQRSRequestName=cQRSRequestName;
            RequestType=requestType;
        }

        public string MethodName { get; set; }

        public string CQRSRequestName { get; set; }

        public string RequestType { get; set; }
    }
}
