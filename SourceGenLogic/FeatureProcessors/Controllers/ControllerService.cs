﻿using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Generators.CQRS.Templates.Commands;
using Restarted.Generators.Generators.CQRS.Templates.Queries;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Definitions.Extensions;
using Restarted.Generators.Generators.CQRS.Models;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.Controllers.Templates;
using System.IO;
using Restarted.Generators.Generators.Repositories.Service.Template;
using System.Runtime.InteropServices.ComTypes;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Models.Metadata;
using Restarted.Generators.FeatureProcessors.CQRS;

namespace Restarted.Generators.FeatureProcessors.Controllers
{
    public class ControllerService
    {

        private const string ConstFeatureName = "featureName";
        private const string ConstFeatureModuleName = "featureModuleName";
        private const string ConstControllerName = "controllerName";
        private const string ConstImplementation = "implementation";
        private const string ConstMethodLevelGeneratedApiCQRSAttribute = "GenerateApiCQRS";
        private const string ConstHttpAction = "action";
        private const string ConstActionRoute = "route";
        private const string InterfaceMethodName = "methodName";
        private const string ConstCQRSRequestName = "cqrsRequestName";
        private const string ConstGeneratorAPIAttribute = "GeneratorApi";

        public static List<string> Process(TypeDefinitionInfo typeDefinitionInfo, GeneratorContext generatorContext, AttributeMetaDataController data)
        {

            List<string> files = new List<string>();


            //data.EntityName
            //data.PluralName,
            //    data.NameSpace
            //    data.ControllerName
            //    data.GenerationPath
            //    data.Actions

            ApiTemplateParameter parameter = new ApiTemplateParameter(typeDefinitionInfo, data.NameSpace, data.ControllerName,  data.EntityName, data.PluralName, data.Actions);

            // Process Validator
            ProcessResult processResult = ProcessControllerTemplate(parameter, typeDefinitionInfo);

            string pathGenerated = FileService.GenerateSourceAtFolderLocation(data.GenerationPath, processResult.FileName, processResult.SourceCode);
            files.Add(pathGenerated);

            //GENERATORCONTEXT Adding namesapces
            //generatorContext.NameSpaces.Add(new NameSpaceInfo( TypeOfCode.Controller, controller, param.PreferredNameSpace, processResult.FileName));




            return files;

        }
        private static ProcessResult ProcessControllerTemplate(ApiTemplateParameter templateParameter, TypeDefinitionInfo typeDefinitionInfo)
        {
            ITemplateParameter parameter = templateParameter;
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new ModelsController(parameter);
            IProcessorResult result = processor.Process(codeTemplate);

            return new ProcessResult(templateParameter.SourceFileName, result.SourceCode);

        }

        
    }
    public class AttributeMetaDataController : AttributeMetaData
    {
        public string ControllerName { get; set; }
        public string EntityName { get; set; }
        public string PluralName { get; set; }

        public string NameSpace { get; set; }



        public List<ControllerAction> Actions { get; set; }
    }
}
