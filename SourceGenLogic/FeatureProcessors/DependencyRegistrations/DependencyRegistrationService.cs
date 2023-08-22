using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.Generators.Dependencies;
using Restarted.Generators.Processor;
using Restarted.Generators.Processor.Interfaces;

namespace Restarted.Generators.FeatureProcessors
{
    public static class DependencyRegistrationService
    {

        public static List<string> Process(GeneratorContext generatorContext,AttributeMetaDataDI data)
        {


            List<string> files = new();

          
            // Generate FileName
            string fileName = data.FileName;

            // Transform Template
            var result = ProcessDependencyRegistrationTemplate(data.NameSpace, fileName, generatorContext.DependencyRepositories);

            var finalReplacedPath = FileService.ConventionBasedPath(data.PathConvention.ConventionPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");

            string pathGenerated = FileService.GenerateSourceAtFolderLocation(finalReplacedPath, fileName, result.SourceCode);

            files.Add(pathGenerated);



            return files;


        }
        private static IProcessorResult ProcessDependencyRegistrationTemplate(string nameSpace, string sourceFileName, Dictionary<string, string> targetSourceMap)
        {
            ITemplateParameter parameter = new DependencyRegistrationTemplateParameter(nameSpace, sourceFileName, targetSourceMap);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new DependencyRegistrationTemplate(parameter);
            return processor.Process(codeTemplate);
        }
        public class AttributeMetaDataDI : AttributeMetaData
        {
            public string FileName { get; set; }
            public string NameSpace { get; set; }
        }

    }
}
