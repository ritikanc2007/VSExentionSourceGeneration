using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.Common.Configurations;
using System.Collections.Immutable;
using Restarted.Generators.Generators.MapperProfiles;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.FeatureProcessors.Process;

namespace Restarted.Generators.FeatureProcessors.MapperProfiles
{
    public static class MapperProfileService
    {

        public static List<string> Process(GeneratorContext generatorContext, AttributeMetaDataMapper data)
        {


            List<string> files = new();

                     

            // Generate FileName
            string fileName = data.FileName;


            var finalReplacedPath = FileService.ConventionBasedPath(data.NameSpace, data.PathConvention.ConventionPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");

            // Transform Template
            var result = ProcessMapperProfileTemplate(finalReplacedPath.NameSpacePath, fileName, generatorContext.MapperProfiles, data.IsMethodGeneration == "true");



            string pathGenerated = FileService.GenerateSourceAtFolderLocation(finalReplacedPath.FolderPath, fileName, result.SourceCode);

            files.Add(pathGenerated);

           

            return files;


        }
        private static IProcessorResult ProcessMapperProfileTemplate(string nameSpace, string sourceFileName, Dictionary<string,string> targetSourceMap, bool isMethodGeneration)
        {
            ITemplateParameter parameter = new MapperProfileTemplateParameter(nameSpace, sourceFileName, targetSourceMap, isMethodGeneration);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new MapperProfileTemplate(parameter);
            //return processor.Process(codeTemplate);
            var func = () =>
            {
                if (TemplateToUse.SwitchFlag == TemplateType.T4)
                    return processor.Process(codeTemplate);
                else
                    return StaticTemplateProcessor.Process(TypeOfTemplate.Mapper, parameter);
            };
            IProcessorResult result = func();
            return result;
        }
        public class AttributeMetaDataMapper : AttributeMetaData
        {

            public string FileName { get; set; }
            public string NameSpace { get; set; }
            public string ProjectName { get; set; }
        }
    }
}
